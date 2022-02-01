﻿using AutoMapper;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.AutoMapper
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<UserViewModel, User>().ForMember(x => x.Password, x => x.Ignore());
            CreateMap<PersonViewModel, Person>();
            CreateMap<ClientViewModel, Client>();
            CreateMap<SpecificationViewModel, Specification>();
            CreateMap<EquipamentViewModel, Equipament>();
            CreateMap<CalendarViewModel, Calendar>();
        }
    }
}
