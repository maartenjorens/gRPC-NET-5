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
            var client = new Beer.V1.Beers.BeersClient(channel);

            await GetBeer(client);
            await GetBeers(client);

            var clientv2 = new Beer.V2.Beers.BeersClient(channel);
            var replyv2 = clientv2.GetBeer(new Beer.V2.BeerRequest
            {
                Id = 1
            });

            System.Console.WriteLine(replyv2);
        }

        private static async Task GetBeer(Beer.V1.Beers.BeersClient client)
        {
            var request = new Beer.V1.BeerRequest()
            {
                Name = "Duvel"
            };

            var reply = await client.GetBeerAsync(request);

            System.Console.WriteLine(reply);
        }

        private static async Task GetBeers(Beer.V1.Beers.BeersClient client)
        {
            using (var call = client.GetBeers(new Google.Protobuf.WellKnownTypes.Empty()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    Beer.V1.BeerReply beerReply = call.ResponseStream.Current;
                    System.Console.WriteLine(beerReply);
                }
            }

        }
    }
}
