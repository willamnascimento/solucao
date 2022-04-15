using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Solucao.Application.Data;
using Solucao.Application.Data.Interfaces;
using Solucao.Application.Data.Repositories;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Test
{
    public class BaseTest
    {
        public IMD5Service mD5Service;
        public TokenService tokenService;
        public IPersonService personService;
        public IClientService clientService;
        public Mock<SolucaoContext> mockSolucaoContext;
        public Mock<IPersonRepository> mockPersonRepository;
        public Mock<IClientRepository> mockClientRepository;
        public Mock<IMapper> mockMapper;
        public DbContextOptions<SolucaoContext> options;
        public PersonRepository personRepository;

        public BaseTest()
        {
            mockMapper = new Mock<IMapper>();
            InjectRepositories();
            InjectServices();

        }

        public void InjectRepositories()
        {
            options = new DbContextOptionsBuilder<SolucaoContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            var service_ = new ServiceCollection();
            service_.AddScoped<SolucaoContext>();
            service_.AddScoped<IPersonRepository, PersonRepository>();
            service_.AddScoped<IClientRepository, ClientRepository>();

            var provider = service_.BuildServiceProvider();
            mockSolucaoContext = new Mock<SolucaoContext>();
            mockPersonRepository =  new Mock<IPersonRepository>();
            mockClientRepository = new Mock<IClientRepository>();
            //personRepository = new PersonRepository(mockSolucaoContext.Object);

        }
        public void InjectServices()
        {
            var service_ = new ServiceCollection();
            service_.AddScoped<IMD5Service, MD5Service>();
            service_.AddScoped<IPersonService, PersonService>();
            service_.AddScoped<TokenService>();

            var provider = service_.BuildServiceProvider();
            mD5Service = provider.GetService<IMD5Service>();
            tokenService = provider.GetService<TokenService>();
            personService = new PersonService(mockPersonRepository.Object, mockMapper.Object);
            clientService = new ClientService(mockClientRepository.Object, mockMapper.Object);

        }
    }
}
