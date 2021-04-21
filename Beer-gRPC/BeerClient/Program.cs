using BeerService;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace BeerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            await GetBeer(channel);
        }

        private static async Task GetBeer(GrpcChannel channel)
        {
            var client = new Beers.BeersClient(channel);

            var request = new BeerRequest()
            {
                Name = "Duvel"
            };

            var reply = await client.GetBeerAsync(request);

            System.Console.WriteLine(reply);
        }
    }
}
