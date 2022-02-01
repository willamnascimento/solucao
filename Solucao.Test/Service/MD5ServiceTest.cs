using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using System;

namespace Solucao.Test.Service
{
    [TestClass]
    public class MD5ServiceTest
    {
        readonly IMD5Service service;
        public MD5ServiceTest()
        {
            var service_ = new ServiceCollection();
            service_.AddScoped<IMD5Service,MD5Service>();

            var provider = service_.BuildServiceProvider();
            service = provider.GetService<IMD5Service>();
        }

        [TestMethod]
        public void ReturnMD5_Success()
        {
            //Arrange
            var input = "123456";

            //Act
            var result = service.ReturnMD5(input);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReturnMD5_IsNull()
        {
            //Arrange

            //Act

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => service.ReturnMD5(null));
        }

        [TestMethod]
        public void CompareMD5_True()
        {
            //Arrange
            var input = "123456";
            var md5 = "e10adc3949ba59abbe56e057f20f883e";

            //Act
            var result = service.CompareMD5(input,md5);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CompareMD5_False()
        {
            //Arrange
            var input = "123451";
            var md5 = "e10adc3949ba59abbe56e057f20f883e";

            //Act
            var result = service.CompareMD5(input, md5);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
