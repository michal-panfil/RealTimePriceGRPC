using Grpc.Core;
using Grpc.Net.Client;
using PriceGrpcService;
using System;
using System.Threading.Tasks;

namespace gRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new PriceProvider.PriceProviderClient(channel);

            var input1 = new PriceRequest{Id = 1};
            var input2 = new PriceRequest{Id = 2};

            var response = await client.GetBasePriceAsync(input1);

                Console.WriteLine("Single price :");
            Console.WriteLine($"Product Id : {response.Id}");
            Console.WriteLine($"Price : {response.Price}");

            Console.WriteLine("==============");
            Console.WriteLine();
            Console.WriteLine("Stream of prices");
            using (var call = client.GetRealTimePrices(input2))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var current = call.ResponseStream.Current;
                    Console.WriteLine($"id : {current.Id}");
                    Console.WriteLine($"price : {current.Price}");
                    Console.WriteLine("----");
                }
            }
            Console.ReadLine();

        }

    }
}
