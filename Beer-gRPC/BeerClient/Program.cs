using BeerService;
using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace BeerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Beers.BeersClient(channel);

            await GetBeer(client);
            await GetBeers(client);
        }

        private static async Task GetBeer(Beers.BeersClient client)
        {
            var request = new BeerRequest()
            {
                Name = "Duvel"
            };

            var reply = await client.GetBeerAsync(request);

            System.Console.WriteLine(reply);
        }

        private static async Task GetBeers(Beers.BeersClient client)
        {
            using (var call = client.GetBeers(new Google.Protobuf.WellKnownTypes.Empty()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    BeerReply beerReply = call.ResponseStream.Current;
                    System.Console.WriteLine(beerReply);
                }
            }

        }
    }
}
