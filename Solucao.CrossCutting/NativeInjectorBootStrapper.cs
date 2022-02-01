using Microsoft.Extensions.DependencyInjection;
using Solucao.Application.Data;
using Solucao.Application.Data.Repositories;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using System;
using System.Net.Http;

namespace Solucao.CrossCutting
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            // Services
            services.AddScoped<IMD5Service, MD5Service>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISpecificationService, SpecificationService>();
            services.AddScoped<IEquipamentService, EquipamentService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<TokenService>();

            // Infra
            services.AddScoped<UserRepository>();
            services.AddScoped<PersonRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<StateRepository>();
            services.AddScoped<CityRepository>();
            services.AddScoped<SpecificationRepository>();
            services.AddScoped<EquipamentRepository>();
            services.AddScoped<EquipamentSpecificationsRepository>();
            services.AddScoped<CalendarRepository>();
            services.AddScoped<SolucaoContext>();


        }
    }
}
