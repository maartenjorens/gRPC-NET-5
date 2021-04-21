using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerService
{
    public class BeerService : Beers.BeersBase
    {
        private readonly ILogger<BeerService> _logger;
        public BeerService(ILogger<BeerService> logger)
        {
            _logger = logger;
        }

        public override Task<BeerReply> GetBeer(BeerRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Requesting beer with name: {name}", request.Name);
            return Task.FromResult(new BeerReply
            {
                Id = 1,
                Name = "Duvel",
                AlcoholPercentage = 8.5,
                EBC = 5.7,
                EBU = 32
            });
        }
    }
}
