using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solucao.API;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Interfaces;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Test.Service
{
    [TestClass]
    public class ClientServiceTest : BaseTest
    {
        [TestInitialize]
        public void Init()
        {

            mockClientRepository.Setup(m => m.GetAll(true, "T"))
                .ReturnsAsync(new List<Client> {
                new Client{ Active = true, Id = new Guid()} });

            mockClientRepository.Setup(m => m.GetById("T"))
                .ReturnsAsync(new  Client { Active = true, Id = new Guid()} );

            mockClientRepository.Setup(m => m.Add(It.IsAny<Client>()))
                .ReturnsAsync(ValidationResult.Success);

            mockClientRepository.Setup(m => m.Update(It.IsAny<Client>()))
                .ReturnsAsync(ValidationResult.Success);


        }

        [TestMethod]
        public async Task GetAll()
        {

            //Arrange
            var active = true;
            var search = "test";


            //Act
            var result = await clientService.GetAll(active, search);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetById()
        {

            //Arrange
            var search = "test";


            //Act
            var result = await clientService.GetById(search);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Add()
        {
            //Arrange
            var model = new ClientViewModel
            {
                Active = true,
                CellPhone = "44923693699",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Teste",
            };

            //Act
            var result = await clientService.Add(model);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update()
        {
            //Arrange
            var model = new ClientViewModel
            {
                Active = false,
                CellPhone = "44923693699",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Teste"
            };

            //Act
            var result = await clientService.Update(model);

            //Assert
            Assert.IsNull(result);
        }
    }
}
