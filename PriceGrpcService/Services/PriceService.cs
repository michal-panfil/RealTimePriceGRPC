using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceGrpcService.Services
{
   public class PriceService : PriceProvider.PriceProviderBase
    {
        private readonly ILogger<PriceService> logger;

        public PriceService(ILogger<PriceService> logger)
        {
            this.logger = logger;
        }

        public override Task<PriceReply> GetBasePrice(PriceRequest request, ServerCallContext context)
        {
            var reply = new PriceReply();
            reply.Id = 100;
            reply.Price = "543.25";

            //if(PriceRequest)
            return Task.FromResult(reply);
        }
    }
}
