using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solucao.Application.Data;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solucao.Test.Repository
{
    [TestClass]
    public class PersonRepositoryTest : BaseTest
    {

        [TestInitialize]
        public void Init()
        {
            using (var context = new SolucaoContext(options))
            {
                context.People.Add(new Person { Id = Guid.NewGuid(), Name = "TV", Active = true, CellPhone = "44696964999", CreatedAt = DateTime.Now, PersonType = "T" });
                context.SaveChanges();
            }

        }

        [TestMethod]
        public async Task GetAll()
        {
            //Arrange
            IEnumerable<Person> result;
            var active = true;
            var search = "test";

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);
                
                //Act
                result = await repository.GetAll(active, search);

            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAll_TipoPessoNull()
        {
            //Arrange
            IEnumerable<Person> result;
            var active = true;

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);

                //Act
                result = await repository.GetAll(active, null);

            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetByName()
        {
            //Arrange
            IEnumerable<Person> result;
            var name = "t";
            var personType = "T";

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);

                //Act
                result = await repository.GetByName(personType, name);

            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task AddPerson()   
        {
            //Arrange
            ValidationResult result;
            var name = "t";
            var personType = "T";
            var person = new Person
            {
                Active = true,
                CellPhone = "44963269874",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "Test",
                PersonType = "T",
                Plate = ""
                
            };

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);

                //Act
                result = await repository.Add(person);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddPersonTryCatch()
        {
            //Arrange
            ValidationResult result;
            var person = new Person
            {
                Active = true,
                CellPhone = "44963269874",
                Id = Guid.NewGuid(),
                Name = "Test",
                PersonType = "T",
                Plate = ""

            };

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);

                //Act
                result = await repository.Add(person);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdatePerson()
        {
            //Arrange
            ValidationResult result;
            

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);

                var item = await repository.GetAll(true, "T");
                var updatedItem = item.FirstOrDefault();
                updatedItem.UpdatedAt = DateTime.Now;
                //Act
                result = await repository.Update(updatedItem);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdatePersonTryCatch()
        {
            //Arrange
            ValidationResult result;
            

            using (var context = new SolucaoContext(options))
            {
                var repository = new PersonRepository(context);
                var item = await repository.GetAll(true, "T");
                var updatedItem = item.FirstOrDefault();
                updatedItem.UpdatedAt = DateTime.Now;
                updatedItem.PersonType = null;

                //Act
                result = await repository.Update(updatedItem);

            }

            //Assert
            Assert.IsNull(result);
        }
    }
}
