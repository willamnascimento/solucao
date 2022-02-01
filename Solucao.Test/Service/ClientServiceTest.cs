using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solucao.API;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Interfaces;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Test.Service
{
    [TestClass]
    public class ClientServiceTest
    {
        readonly IServiceProvider _services = Program.CreateHostBuilder(new string[] { }).Build().Services; // one liner
        readonly IClientService service;
        public ClientServiceTest()
        {
            service = _services.GetRequiredService<IClientService>();
        }

        //[TestMethod]
        public async Task GetMyTest()
        {

            //Arrange
            var active = true;
            var search = "test";
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(repo => repo.GetAll(active, search))
                .ReturnsAsync(new List<Client>{ new Client { Active = true, Id = new Guid() } 
                });

            //Act
            var result = await service.GetAll(active,search);

            //Assert
            Assert.IsNotNull(service);
        }
    }
}
