using DataProviders.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders
{
    public class LinePageDataProvider : IDataProvider
    {
        private readonly ILogger<LinePageDataProvider> logger;

        public LinePageDataProvider(ILogger<LinePageDataProvider> logger)
        {
            this.logger = logger;
        }

        public Task<object> GetData()
        {
            return Task.FromResult(GenerateLinePage() as object);
        }

        private LinePage GenerateLinePage()
        {
            var rnd = new Random();
            return new LinePage()
            {
                Fact = rnd.Next(),
                PlanPredict = rnd.Next(),
                Plan = rnd.Next(),
                PercentageInfluence = rnd.NextDouble(),
                DeviationPlanPredictFact = rnd.Next(),
                DeviationPlanPredict = rnd.Next(),
                Predict = rnd.Next()
            };
        }
    }
}
