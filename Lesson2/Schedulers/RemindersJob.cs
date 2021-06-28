using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Shared.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Shared.Services.Json.Services;
using Shared.Models;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Shared.DBContext;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace Lesson2.Schedulers
{
    public class RemindersJob : IJob
    {
        private readonly IServiceProvider provider;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly int takeEvents = 100;
        private readonly int numThreads = 5;
        private readonly string serviceUrl;
        private readonly string loginUlr;
        public RemindersJob(IServiceProvider provider,
            ILogger<RemindersJob> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.provider = provider;
            this.logger = logger;
            this.configuration = configuration;
            this.serviceScopeFactory = serviceScopeFactory;
            this.serviceUrl = configuration["ServiceUrl"];
            this.loginUlr = configuration["LoginUrl"];
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var token = await LogInService();
            if (string.IsNullOrEmpty(token))
            {
                logger.LogError("Error authentication");
                return;
            }

            var unitIds = configuration.GetSection("UnitsID").Get<int[]>();

            if (unitIds.Length == 0)
            {
                return;
            }

            var tasks = unitIds.Select(unitId => (Func<Task>)(() => UpdateEventsAsync(unitId, token))).ToList();
            await tasks.RunInParallel(logger, numThreads);

        }

        private async Task<string> LogInService()
        {
            using HttpClient httpClient = new HttpClient();
            var loginDataJson = JsonConvert.SerializeObject(new
            {
                login = "admin",
                password = "pwd123"
            });
            var content = new StringContent(loginDataJson, Encoding.UTF8, "application/json");
            var responce = await httpClient.PostAsync($"{loginUlr}api/User/auth", content);
            if (responce.IsSuccessStatusCode)
            {
                return await responce.Content.ReadAsStringAsync();
            }
            return null;
        }

        private async Task UpdateEventsAsync(int unitId, string token)
        {
            IList<Func<Task>> tasks = new List<Func<Task>>();
            int skip = 0;

            while (true)
            {
                var events = await GetEvents(unitId, takeEvents, skip, token);
                if (events.Count == 0)
                {
                    break;
                }
                tasks.Add((Func<Task>)(() => DoWork(events)));
                skip += takeEvents;
            }
            await tasks.RunInParallel(logger, numThreads);
        }

        public async Task DoWork(IList<Event> events)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

                foreach (var e in events)
                {

                    Event existEvent = dbContext.Set<Event>().Find(e.Id);
                    if (existEvent == null)
                    {
                        dbContext.Set<Event>().Add(e);
                    }
                    else
                    {
                        if (e.IsActive)
                        {
                            LogUpdatedEvent(dbContext, existEvent, e);
                            dbContext.Entry(existEvent).CurrentValues.SetValues(e);
                        }
                    }


                }
                await dbContext.SaveChangesAsync();
            }

        }

        public void LogUpdatedEvent(DataBaseContext dbContext, Event oldEvent, Event newEvent)
        {
            PropertyInfo[] properties = typeof(Event).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var oldValue = property.GetValue(oldEvent).ToString();
                var newValue = property.GetValue(newEvent).ToString();
                if (oldValue != newValue)
                {
                    UpdateModel um = new UpdateModel
                    {
                        Timestamp = DateTime.Now,
                        FieldName = property.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                        EventId = oldEvent.Id
                    };
                    dbContext.Set<UpdateModel>().Add(um);
                }
            }

        }

        public async Task<IList<Event>> GetEvents(int unitId, int take, int skip, string token)
        {
            using HttpClient httpClient = new HttpClient();
            var unitsIdUrl = $"{serviceUrl}api/events/keys?unitId={unitId}&take={take}&skip={skip}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGet = await httpClient.GetAsync(unitsIdUrl);
            if (!responseGet.IsSuccessStatusCode)
            {
                logger.LogError("Error authentication");
                return new List<Event>();
            }
            var result = await responseGet.Content.ReadAsStringAsync();
            var data = new StringContent(result, Encoding.UTF8, "application/json");

            var eventsUrl = $"{ serviceUrl}api/events";
            var responsePost = await httpClient.PostAsync(eventsUrl, data);
            result = responsePost.Content.ReadAsStringAsync().Result;
            var events = JsonConvert.DeserializeObject<IList<Event>>(result);
            return events;

        }


    }
}