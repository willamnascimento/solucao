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

            //return await Db.Clients.Include(x => x.City).Include(x => x.State).Where(x => x.Active == ativo && (x.Address.Contains(search) || x.Email.Contains(search) || x.Name.Contains(search) || x.Phone.Contains(search) || x.CellPhone.Contains(search))).OrderBy(x => x.Name).ToListAsync();
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

    }
}
