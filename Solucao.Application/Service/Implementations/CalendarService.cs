using AutoMapper;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using Solucao.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Implementations
{
    public class CalendarService : ICalendarService
    {
        private CalendarRepository calendarRepository;
        private SpecificationRepository specificationRepository;
        private readonly IMapper mapper;
        public CalendarService(CalendarRepository _calendarRepository, IMapper _mapper, SpecificationRepository _specificationRepository)
        {
            calendarRepository = _calendarRepository;
            mapper = _mapper;
            specificationRepository = _specificationRepository;
        }

        public async Task<IEnumerable<CalendarViewModel>> GetAll(DateTime date)
        {
            return mapper.Map<IEnumerable<CalendarViewModel>>(await calendarRepository.GetAll(date));
        }
        public async Task<CalendarViewModel> GetById(Guid id)
        {
            return mapper.Map<CalendarViewModel>(await calendarRepository.GetById(id));
        }

        public Task<ValidationResult> Add(CalendarViewModel calendar, Guid user)
        {
            calendar.Id = Guid.NewGuid();
            calendar.Client = null;
            calendar.UserId = user;
            calendar.CreatedAt = DateTime.Now;

            if (!string.IsNullOrEmpty(calendar.StartTime1))
            {
                var start = calendar.Date.ToString("yyyy-MM-dd") + " " + calendar.StartTime1.Insert(2, ":");
                calendar.StartTime = DateTime.Parse(start);
            }

            if (!string.IsNullOrEmpty(calendar.EndTime1))
            {
                var end = calendar.Date.ToString("yyyy-MM-dd") + " " + calendar.EndTime1.Insert(2, ":");
                calendar.EndTime = DateTime.Parse(end);
            }

            if (string.IsNullOrEmpty(calendar.Status))
                calendar.Status = "pending";
            
            var _calendar = mapper.Map<Calendar>(calendar);

            return calendarRepository.Add(_calendar);
        }

        public async Task<ValidationResult> Update(CalendarViewModel calendar, Guid user)
        {
            ValidationResult result;
            Guid parentId;

            // Atualiza o registro e inativa a locação
            if (calendar.ParentId != null)
            {
                var temp = await GetById(calendar.Id.Value);
                temp.Active = false;
                temp.UpdatedAt = DateTime.Now;
                temp.Client = null;
                parentId = temp.ParentId.Value;
                calendar.Client = null;
                var temp_ = mapper.Map<Calendar>(temp);

                result = await calendarRepository.Update(temp_);
            }
            else
            {
                calendar.Client = null;
                calendar.UpdatedAt = DateTime.Now;
                calendar.Active = false;
                parentId = calendar.Id.Value;
                var _calendar = mapper.Map<Calendar>(calendar);

                result = await calendarRepository.Update(_calendar);
            }

            if (result == null)
            {
                calendar.ParentId = parentId;
                calendar.Id = null;
                calendar.CreatedAt = DateTime.Now;
                calendar.UpdatedAt = null;
                calendar.Active = true;
                calendar.StartTime = null;
                calendar.EndTime = null;
                calendar.UserId = user;

                if (!string.IsNullOrEmpty(calendar.StartTime1))
                {
                    var start = calendar.Date.ToString("yyyy-MM-dd") + " " + calendar.StartTime1.Replace(":","").Insert(2,":");
                    calendar.StartTime = DateTime.Parse(start);
                }

                if (!string.IsNullOrEmpty(calendar.EndTime1))
                {
                    var end = calendar.Date.ToString("yyyy-MM-dd") + " " + calendar.EndTime1.Replace(":", "").Insert(2, ":");
                    calendar.EndTime = DateTime.Parse(end);
                }

                if (string.IsNullOrEmpty(calendar.Status))
                    calendar.Status = "pending";
                
                var _calendarAdd = mapper.Map<Calendar>(calendar);

                return await calendarRepository.Add(_calendarAdd);

            }

            return null;
        }

        public async Task<ValidationResult> ValidateLease(DateTime date, Guid clientId, Guid equipamentId, IList<CalendarSpecifications> specifications, string startTime)
        {
            try
            {
                var startTime_ = DateTime.Parse(date.ToString("yyyy-MM-dd ") + startTime.Replace(":", "").Insert(2, ":"));

                // Valida se o equipamento ja esta em uso
                var result = await calendarRepository.ValidateEquipament(date, clientId, equipamentId);

                if (!result.Any())
                {
                    // Valida se a ponteira ja esta em uso com outro equipamento
                    // Os equipamentos podem compartilhar a mesma ponteira
                    var temp = specifications.Where(x => x.Active).ToList();

                    var res = await calendarRepository.GetCalendarBySpecificationsAndDate(temp, date, startTime_);

                    if (res.Any())
                    {
                        foreach (var item in temp)
                        {
                            var spec = await specificationRepository.GetById(item.SpecificationId);
                            var counter = await calendarRepository.SpecCounterBySpec(item.SpecificationId, date, startTime_);

                            if (counter >= spec.Amount )
                                return new ValidationResult($"Para data e hora informada, ponteira já está em uso. ({spec.Name})");
                        }
                    }

                    // Valida se a ponteira/especificação é unica
                    if (specifications.Any())
                    {
                        var valid = await ValidIfSpecInUse(temp, date);
                        if (!valid)
                            return new ValidationResult($"Para data e hora informada, dispositivo ÚNICO está em uso.");

                    }

                    return ValidationResult.Success;

                }

                foreach (var item in result)
                {
                    if (startTime_ >= item.StartTime && startTime_ <= item.EndTime)
                        return new ValidationResult("Para data e hora informada, equipamento já está em uso.");

                    if (item.EndTime.HasValue)
                    {
                        var time = startTime_ - item.EndTime.Value;
                        double minutes = 0;
                        if (time.TotalMinutes < 0)
                            minutes = time.TotalMinutes * -1;
                        else
                            minutes = time.TotalMinutes;

                        if (minutes < 60)
                            return new ValidationResult("Diferença da locação do equipamento menor que 60 minutos.");

                    }

                }

                return ValidationResult.Success;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CalendarViewModel>> Availability(DateTime startDate, DateTime endDate, Guid? clientId, Guid? equipamentId)
        {
            return mapper.Map<IEnumerable<CalendarViewModel>>(await calendarRepository.Availability(startDate, endDate, clientId, equipamentId));
        }

        private async Task<bool> ValidIfSpecInUse(IList<CalendarSpecifications> specifications, DateTime date)
        {
            var singleSpec = await specificationRepository.GetSingleSpec();

            if (singleSpec != null)
            {
                if (specifications.Any(x => x.SpecificationId == singleSpec.Id))
                {
                    var resp = await calendarRepository.SingleSpecCounter(singleSpec.Id, date);

                    if (resp > 0)
                        return false;
                }
            }
            return true;
        }
    }
}
