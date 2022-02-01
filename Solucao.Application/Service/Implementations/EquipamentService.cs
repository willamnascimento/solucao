﻿using AutoMapper;
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
    public class EquipamentService : IEquipamentService
    {
        private EquipamentRepository equipamentRepository;
        private EquipamentSpecificationsRepository equipamentSpecificationsRepository;
        private readonly IMapper mapper;

        public EquipamentService(EquipamentRepository _equipamentRepository, EquipamentSpecificationsRepository _equipamentSpecificationsRepository,IMapper _mapper)
        {
            equipamentRepository = _equipamentRepository;
            equipamentSpecificationsRepository = _equipamentSpecificationsRepository;
            mapper = _mapper;
        }
        public async Task<ValidationResult> Add(EquipamentViewModel equipament)
        {
            equipament.Id = Guid.NewGuid();
            equipament.CreatedAt = DateTime.Now;
            var _equipament = mapper.Map<Equipament>(equipament);
            return await equipamentRepository.Add(_equipament);
        }

        public async Task<IEnumerable<EquipamentViewModel>> GetAll(bool ativo)
        {
            return mapper.Map<IEnumerable<EquipamentViewModel>>(await equipamentRepository.GetAll(ativo));
        }

        public Task<ValidationResult> Update(EquipamentViewModel equipament)
        {
            var _equipament = mapper.Map<Equipament>(equipament);

            _equipament.UpdatedAt = DateTime.Now;

            return equipamentRepository.Update(_equipament);
        }
    }
}
