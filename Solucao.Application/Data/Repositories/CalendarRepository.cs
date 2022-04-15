using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Repositories
{
    public class CalendarRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<Calendar> DbSet;
        public CalendarRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<Calendar>();
        }

        public async Task<IEnumerable<Calendar>> GetAll(DateTime date)
        {
            
                return await Db.Calendars.Include(x => x.Equipament)
                                         .Include(x => x.Client)
                                         .Include(x => x.Driver)
                                         .Include(x => x.Technique)
                                         .Include(x => x.User)
                                         .Include(x => x.CalendarSpecifications)
                                         .Where(x => x.Date.Date == date && x.Active).OrderBy(x => x.Client.Name).ToListAsync();

        }

        public async Task<Calendar> GetById(Guid id)
        {
            return await Db.Calendars.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ValidationResult> Add(Calendar calendar)
        {
            try
            {

                Db.Calendars.Add(calendar);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<ValidationResult> Update(Calendar calendar)
        {
            try
            {
                DbSet.Update(calendar);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

        }

        public async Task<IEnumerable<Calendar>> ValidateLease(DateTime date, Guid clientId, Guid equipamentId)
        {
            
            var sql = $"select * from Calendars where date >= '{date.ToString("yyyy-MM-dd")}' and equipamentId = '{equipamentId}' and ClientId != '{clientId}'";
            return await Db.Calendars.FromSqlRaw(sql).ToListAsync();

        }

        public async Task<IEnumerable<Calendar>> GetCalendarBySpecificationsAndDate(List<CalendarSpecifications> list, DateTime date)
        {
            var _in = list.Select(x => x.SpecificationId);

            var a = await (from calendar in Db.Calendars
                    join specification in Db.CalendarSpecifications on calendar.Id equals specification.CalendarId
                    where
                    calendar.Date == date &&
                    _in.Contains(specification.SpecificationId)
                    select calendar).Include(x => x.CalendarSpecifications).ToListAsync();

            return a;


                //.Include(x => x.CalendarSpecifications)
                //                          .Where(x => x.Date.Date >= date.Date && x.CalendarSpecifications.Contains(_in)
                //                                      x.Active).OrderBy(x => x.Date).ToListAsync();
        }

        public async Task<IEnumerable<Calendar>> Availability(DateTime startDate, DateTime endDate,  Guid? clientId, Guid? equipamentId)
        {

            if (clientId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.ClientId == clientId.Value && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }

            if (equipamentId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.EquipamentId == equipamentId.Value && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }

            if (equipamentId.HasValue && clientId.HasValue)
            {
                return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.EquipamentId == equipamentId.Value && 
                                                      x.ClientId == clientId.Value &&
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();
            }
            return await Db.Calendars.Include(x => x.Equipament)
                                          .Include(x => x.Client)
                                          .Include(x => x.Driver)
                                          .Include(x => x.Technique)
                                          .Include(x => x.User)
                                          .Include(x => x.CalendarSpecifications)
                                          .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate && 
                                                      x.Active).OrderBy(x => x.Date).ToListAsync();

        }

    }
}
