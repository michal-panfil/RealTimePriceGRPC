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

            var input = new PriceRequest
            {
                Id = 1
            };

            var response = await client.GetBasePriceAsync(input);


                Console.WriteLine($"{response.Id} : {response.Price}");
            Console.ReadLine();
        }
    }
}
