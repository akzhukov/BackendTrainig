using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2.Service
{
    public class UpdateVolumeService : IHostedService, IDisposable
    {
        private readonly ILogger<UpdateVolumeService> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private Timer timer;

        public UpdateVolumeService(ILogger<UpdateVolumeService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Update Volume Service running");

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Update Volume Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ITankRepository>();

                foreach (var tank in repository.GetAll().ToList())
                {
                    
                    var newVolume = await repository.RandomVolumeUpdate(tank.Id);
                    if (newVolume > tank.MaxVolume)
                    {
                        logger.LogInformation($"Error! The tank with ID {tank.Id} is more full above the maximum value");
                    }
                }

            }

        }
    }
}
