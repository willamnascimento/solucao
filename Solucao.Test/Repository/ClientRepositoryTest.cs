using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solucao.Application.Data;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Test.Repository
{
    [TestClass]
    public class ClientRepositoryTest : BaseTest
    {

        public ClientRepositoryTest()
        {
            using (var context = new SolucaoContext(options))
            {
                Random random = new Random();
                
                context.Clients.Add(new Client
                {   
                    Id = Guid.NewGuid(),
                    Name = "TV",
                    Active = true,
                    CellPhone = "44696964999",
                    CreatedAt = DateTime.Now,
                    City = new City { Id = random.Next(100000,200000), Nome = "1" },
                    CityId = 1523631,
                    State = new State { Id = random.Next(100000, 200000), Nome = "1" },
                    StateId = 22631,
                    Address = "teste",
                    Complement = "test",
                    Email = "test@test.com",
                    Number = "SN",
                    Phone = "436363212525"

                });
                context.SaveChanges();
            }
        }
        

        [TestMethod]
        public async Task GetAll()
        {
            //Arrange
            IEnumerable<Client> result;
            var active = true;
            var search = "test";

            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);

                //Act
                result = await repository.GetAll(active, search);

            }

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetById()
        {
            //Arrange
            Client result;
            var id = "FBB52B53-2CC0-4663-918F-09987DC651A0";

            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);

                //Act
                result = await repository.GetById(id);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddPerson()
        {
            //Arrange
            ValidationResult result;
            var person = new Client
            {
                Active = true,
                CellPhone = "44963269874",
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                StateId = 1,
                CityId = 2,
                Name = "Test"

            };

            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);

                //Act
                result = await repository.Add(person);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddClientTryCatch()
        {
            //Arrange
            ValidationResult result;
            var client = new Client
            {
                Active = true,
                CellPhone = "44963269874",
                Id = Guid.NewGuid(),
                Name = null
            };

            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);

                //Act
                result = await repository.Add(client);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateClient()
        {
            //Arrange
            ValidationResult result;


            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);

                var item = context.Clients.FirstOrDefault();
                item.UpdatedAt = DateTime.Now;
                //Act
                result = await repository.Update(item);

            }

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateClientTryCatch()
        {
            //Arrange
            ValidationResult result;


            using (var context = new SolucaoContext(options))
            {
                var repository = new ClientRepository(context);
                var item = context.Clients.FirstOrDefault();
                item.UpdatedAt = DateTime.Now;
                item.Name = null;
                //Act
                result = await repository.Update(item);

            }

            //Assert
            Assert.IsNull(result);
        }
    }
}
