using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solucao.Application.Contracts;
using Solucao.Application.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Test.Service
{
    [TestClass]
    public class TokenServiceTest : BaseTest
    {

        [TestMethod]
        public void GenerateToken_Success()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Active = true,
                CreatedAt = DateTime.Now,
                Email = "test@test.com",
                Id = new Guid(),
                Name = "Test",
                Role = "admin"
            };

            //Act
            var result = tokenService.GenerateToken(userViewModel);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GenerateToken_IsNull()
        {
            //Arrange

            //Act
            var result = tokenService.GenerateToken(null);

            //Assert
            Assert.IsNull(result);
        }

    }
}
