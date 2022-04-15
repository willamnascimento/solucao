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
    public class SpecificationRepository
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<Specification> DbSet;

        public SpecificationRepository(SolucaoContext _context)
        {
            Db = _context;
            DbSet = Db.Set<Specification>();
        }

        public async Task<IEnumerable<Specification>> GetAll()
        {
            return await Db.Specifications.Include(x => x.EquipamentSpecifications).ToListAsync();
        }

        public async Task<bool> SingleIsValid(Guid? id)
        {
            if (id.HasValue)
                return await Db.Specifications.AnyAsync(x => x.Id != id.Value && x.Active && x.Single);
            return await Db.Specifications.AnyAsync(x => x.Active && x.Single);
        }

        public async Task<ValidationResult> Add(Specification specification)
        {
            try
            {

                Db.Specifications.Add(specification);
                await Db.SaveChangesAsync();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<ValidationResult> Update(Specification specification)
        {
            try
            {
                DbSet.Update(specification);
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
