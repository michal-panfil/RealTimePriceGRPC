﻿using Grpc.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PriceGrpcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceGrpcService.Services
{
    public class PriceService : PriceProvider.PriceProviderBase
    {
        private readonly ILogger<PriceService> logger;
        private readonly IDataDownloader downloader;
        private readonly IDataParser parser;

        public PriceService(ILogger<PriceService> logger, IDataDownloader downloader, IDataParser parser)
        {
            this.logger = logger;
            this.downloader = downloader;
            this.parser = parser;
        }

        public override async Task GetRealTimePricesContinously(PriceRequest request, IServerStreamWriter<InstrumrntPriceReply> responseStream, ServerCallContext context)
        {

            var count = 0;
            while (count < 100)
            {
                string rawPage = "";
                var tries = 0;
                while (rawPage == "" || tries > 5 || rawPage == null)
                {

                    rawPage = await downloader.DownloadData("https://www.investing.com/commodities/real-time-futures");
                    tries++;
                }

                var instruments = await parser.ParseData(rawPage);

                instruments.ForEach(async i => await responseStream.WriteAsync(new InstrumrntPriceReply
                { Name = i.Name, Price = i.Price, Time = i.Time.ToString() }));

                count++;
            }
        }
        //Demo
        public override Task<PriceReply> GetBasePrice(PriceRequest request, ServerCallContext context)
        {
            var reply = new PriceReply();
            reply.Id = 100;
            reply.Price = "543.25";

            return Task.FromResult(reply);
        }
        //Demo
        public override async Task GetRealTimePrices(PriceRequest request, IServerStreamWriter<PriceReply> responseStream, ServerCallContext context)
        {

            Console.WriteLine("Proccesing : GetRealTimePrices");
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new PriceReply { Id = 1 + 110, Price = (i + 2 * 1.12).ToString() });
            }
        }
        
    }
}
