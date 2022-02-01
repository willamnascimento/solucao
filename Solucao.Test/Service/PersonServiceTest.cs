using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Interfaces;
using Solucao.Application.Data.Repositories;
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
    public class PersonServiceTest : BaseTest
    {
        

        [TestInitialize]
        public void Init()
        {

            mockPersonRepository.Setup(m => m.GetAll(true,"T"))
                .ReturnsAsync(new List<Person> { 
                new Person { Active = true, Id = new Guid()} });

            mockPersonRepository.Setup(m => m.GetByName("T","test"))
                .ReturnsAsync(new List<Person> {
                new Person { Active = true, Id = new Guid()} });

            mockPersonRepository.Setup(m => m.Add(It.IsAny<Person>()))
                .ReturnsAsync(ValidationResult.Success);

            mockPersonRepository.Setup(m => m.Update(It.IsAny<Person>()))
                .ReturnsAsync(ValidationResult.Success);


        }


        [TestMethod]
        public async Task GetAll()
        {
            //Arrange
            var active = true;
            var search = "test";
           

            //Act
            var result = await personService.GetAll(active, search);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetByName()
        {
            //Arrange
            var personType = "T";
            var search = "test";

            //Act
            var result = await personService.GetByName(personType, search);

            //Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task Add()
        {
            //Arrange
            var model = new PersonViewModel
            {
                Active = true,
                CellPhone = "44923693699",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Teste",
                PersonType = "T",
                Plate = ""
            };

            //Act
            var result = await personService.Add(model);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update()
        {
            //Arrange
            var model = new PersonViewModel
            {
                Active = false,
                CellPhone = "44923693699",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Teste",
                PersonType = "M",
                Plate = "AAAA",
            };

            //Act
            var result = await personService.Update(model);

            //Assert
            Assert.IsNull(result);
        }

    }
}
