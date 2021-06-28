using DataProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketsService.Hubs;
using WebSocketsService.Schedulers;

namespace WebSocketsService.Extensions
{
    public static class HubsExtension
    {
        public static void AddHubs(this IServiceCollection services)
        {

            services.AddSingleton<IDataProviderFactory, DataProviderFactory>();

            services.AddSingleton<IDataProvider, SimpleChartDataProvider>();
            services.AddSingleton<IDataProvider, LinePageDataProvider>();

            services.AddSingleton<SimpleChartHub>();
            services.AddHostedService<DistributionBackgroundService<SimpleChartHub>>();
            
            services.AddSingleton<LinePageChartHub>();
            services.AddHostedService<DistributionBackgroundService<LinePageChartHub>>();
        }

        public static void MapHubs(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<SimpleChartHub>("/SimpleChartHub");
            endpoints.MapHub<LinePageChartHub>("/LinePageChartHub");
            
        }
    }
}
