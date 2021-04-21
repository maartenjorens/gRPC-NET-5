using Beer.V2;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerService
{
    public class BeerServiceV2 : Beer.V2.Beers.BeersBase
    {
        private readonly ILogger<BeerServiceV2> _logger;
        public BeerServiceV2(ILogger<BeerServiceV2> logger)
        {
            _logger = logger;
        }

        public override Task<BeerReply> GetBeer(BeerRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Requesting beer with id: {id}", request.Id);
            var bestBeforeDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow.AddDays(30));

            return Task.FromResult(new BeerReply
            {
                Id = 1,
                Name = "Duvel V2 VERSION",
                AlcoholPercentage = 8.5,
                EBC = 5.7,
                EBU = 32,
                BestBefore = bestBeforeDate,
                BeerType = BeerType.BelgianBlond,
                Hops =
                {
                    "citra",
                    "admiral"
                }
            });
        }

        public override async Task GetBeers(
            Google.Protobuf.WellKnownTypes.Empty request, 
            IServerStreamWriter<BeerReply> responseStream, 
            ServerCallContext context)
        {
            var beers = new List<BeerReply>()
            {
                new BeerReply
                {
                    Id = 1,
                    Name = "Duvel",
                    AlcoholPercentage = 8.5,
                    EBC = 5.7,
                    EBU = 32,
                    BestBefore = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow.AddDays(30)),
                    BeerType = BeerType.BelgianBlond,
                    Hops =
                    {
                        "citra",
                        "admiral"
                    }
                },
                new BeerReply
                {
                    Id = 2,
                    Name = "Guinness",
                    AlcoholPercentage = 9,
                    EBC = 5.7,
                    EBU = 32,
                    BestBefore = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow.AddDays(90)),
                    BeerType = BeerType.Stout,
                    Hops =
                    {
                        "admiral"
                    }
                }
            };

            foreach (var beer in beers)
            {
                await responseStream.WriteAsync(beer);
                await Task.Delay(5000);
            }
        }
    }
}
