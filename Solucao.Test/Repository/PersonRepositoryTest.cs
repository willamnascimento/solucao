using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solucao.Application.Data;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using System;
using System.Collections.Generic;
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
                context.People.Add(new Person { Id = Guid.NewGuid(), Name = "TV" });
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
    }
}
