using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PriceGrpcService;
using PriceGrpcService.Models;
using PriceGrpcService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTest
{
    [TestFixture]
    public class PriceServiceTest
    {
        PriceService Service;
        [SetUp]
        public void Setup() {
            Mock<ILogger<PriceService>> logger = new Mock<ILogger<PriceService>>();
            Service = new PriceService(logger.Object, new PageDownloader(), new PageParser() );    
        
        }
        [Test]
        public void GetBasePrice() {
            var request = new PriceRequest();
            var ctx = new Mock<ServerCallContext>();
            var result = Service.GetBasePrice(request, ctx.Object).Result;
            Assert.IsNotNull(result);
        }
    }
}
