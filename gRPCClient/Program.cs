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
            var input1 = new PriceRequest { Id = 1 };

            using ( var call =  client.GetRealTimePricesContinously(input1))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var current = call.ResponseStream.Current;
                    Console.WriteLine($"{current.Name} : {current.Price} : { current.Time} ");
                    Console.WriteLine("------------------------");
                } 

            }
        }

    }
}
