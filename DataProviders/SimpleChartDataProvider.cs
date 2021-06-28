using DataProviders.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProviders
{
    public class SimpleChartDataProvider : IDataProvider
    {
        private readonly ILogger<SimpleChartDataProvider> logger;

        public SimpleChartDataProvider(ILogger<SimpleChartDataProvider> logger)
        {
            this.logger = logger;
        }

        Task<object> IDataProvider.GetData()
        {
            return Task.FromResult(GenerateSimpleChart() as object);
        }

        private SimpleChart GenerateSimpleChart()
        {
            var rnd = new Random();
            int min = rnd.Next(Int32.MinValue, 0);
            int max = rnd.Next(0, Int32.MaxValue);
            int val = rnd.Next(min, max);
            return new SimpleChart()
            {
                Name = "Test",
                Min = min,
                Max = max,
                Value = val,
                Deviation = rnd.Next()
            };
        }
    }
}
