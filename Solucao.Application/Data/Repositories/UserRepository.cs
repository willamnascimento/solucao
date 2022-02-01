﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using NetDevPack.Data;
using Microsoft.Extensions.Logging;

namespace Solucao.Application.Data.Repositories
{
    public class UserRepository 
    {
        public IUnitOfWork UnitOfWork => Db;
        protected readonly SolucaoContext Db;
        protected readonly DbSet<User> DbSet;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(SolucaoContext _context, ILogger<UserRepository> _logger)
        {
            Db = _context;
            DbSet = Db.Set<User>();
            logger = _logger;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Db.Users.ToListAsync();
        }

        public async Task<User> GetById(string Id)
        {
            return await Db.Users.FindAsync(Id);
        }

        public async Task<User> GetByName(string name)
        {
            return await Db.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ValidationResult> Add(User user)
        {
            try
            {
                await Db.Users.AddAsync(user);
                Db.SaveChanges();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                logger.LogError(e.StackTrace);
                return new ValidationResult(e.Message);
            }
        }

        
        public async Task<ValidationResult> Update(User user, string id)
        {
            try
            {
                var _user = await DbSet.FindAsync(id);
                DbSet.Update(_user);
                Db.SaveChanges();
                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                logger.LogError(e.StackTrace);
                return new ValidationResult(e.Message);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            return await Db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Active);

        }

    }
}
