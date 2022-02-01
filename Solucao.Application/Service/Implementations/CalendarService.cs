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
        private readonly IMapper mapper;
        public CalendarService(CalendarRepository _calendarRepository, IMapper _mapper)
        {
            calendarRepository = _calendarRepository;
            mapper = _mapper;
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

        public async Task<ValidationResult> ValidateLease(DateTime date, Guid clientId, Guid equipamentId, string startTime)
        {
            try
            {
                var startTime_ = DateTime.Parse(date.ToString("yyyy-MM-dd ") + startTime.Replace(":", "").Insert(2, ":"));

                var result = await calendarRepository.ValidateLease(date, clientId, equipamentId);

                if (!result.Any())
                    return ValidationResult.Success;

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
    }
}
