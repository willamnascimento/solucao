﻿  using AutoMapper;
using MongoDB.Bson;
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
    public class UserService : IUserService
    {
        private UserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IMD5Service mD5Service;
        public UserService(UserRepository _userRepository, IMapper _mapper, IMD5Service _mD5Service)
        {
            userRepository = _userRepository;
            mapper = _mapper;
            mD5Service = _mD5Service;
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            return mapper.Map<IEnumerable<UserViewModel>>(await userRepository.GetAll());
        }

        public Task<UserViewModel> GetById(string Id)
        {
            return mapper.Map<Task<UserViewModel>>(userRepository.GetById(Id));
        }

        public Task<ValidationResult> Add(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.Now;
            user.Password = mD5Service.ReturnMD5(user.Password);

            return userRepository.Add(user);
        }

        public Task<ValidationResult> Update(User user, string id)
        {
                user.UpdatedAt = DateTime.Now;

                return userRepository.Update(user, id);
        }

        public async Task<UserViewModel> Authenticate(string email, string password)
        {
            var user = await userRepository.GetByEmail(email);

            if (user != null)
            {
                if (mD5Service.CompareMD5(password, user.Password))
                {
                    return mapper.Map<UserViewModel>(user);
                }
            }

            return null;

        }

        public async Task<UserViewModel> GetByName(string Name)
        {
            return mapper.Map<UserViewModel>(await userRepository.GetByName(Name));
        }
    }
}
