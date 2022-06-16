﻿using Microsoft.EntityFrameworkCore;
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
    public class EquipamentRepository 
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<Equipament> DbSet;

        public EquipamentRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<Equipament>();
        }

        public async Task<IEnumerable<Equipament>> GetAll(bool ativo)
        {
            return await Db.Equipaments.Include(x => x.EquipamentSpecifications).Where(x => x.Active == ativo).OrderBy(x => x.Order).ToListAsync();
        }

        public async Task<ValidationResult> Add(Equipament equipament)
        {
            try
            {

                Db.Equipaments.Add(equipament);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<ValidationResult> Update(Equipament equipament)
        {
            try
            {
                Db.EquipamentSpecifications.RemoveRange(Db.EquipamentSpecifications.Where(x => x.EquipamentId == equipament.Id));
                DbSet.Update(equipament);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

        }
    }
}
