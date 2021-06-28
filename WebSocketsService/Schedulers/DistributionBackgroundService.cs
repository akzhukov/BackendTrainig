using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebSocketsService.Hubs;

namespace WebSocketsService.Schedulers
{
    public class DistributionBackgroundService<T> : BackgroundService where T : BaseHub
    {
        private readonly IConfiguration configuration;
        private readonly IHubContext<T> hubContext;
        private readonly T hub;

        public DistributionBackgroundService(IConfiguration configuration, IHubContext<T> hubContext, T hub)
        {
            this.configuration = configuration;
            this.hubContext = hubContext;
            this.hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await hub.Send();
                await Task.Delay(Convert.ToInt32(configuration["Hubs:SendIntervalMillisec"]), stoppingToken);
            }
        }
    }
}
