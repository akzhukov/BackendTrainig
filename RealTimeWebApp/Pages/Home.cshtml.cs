using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealTimeWebApp.HubsManager;
using RealTimeWebApp.Models;
using SocketsShared.DTOs;

namespace RealTimeWebApp.Pages
{
    public class HomeModel : PageModel
    {
        private readonly ILogger<HomeModel> logger;
        private readonly IConfiguration configuration;
        private readonly IHubManager hubManager;
        private readonly string channelsUrl;
        private readonly string subscribeUrl;

        [BindProperty]
        public List<HubDto> Items { get; set; }

        public HomeModel(ILogger<HomeModel> logger, IConfiguration configuration, IHubManager hubManager)
        {
            this.configuration = configuration;
            this.hubManager = hubManager;
            this.channelsUrl = configuration["ChannelsUrl"];
            this.subscribeUrl = configuration["SubscribeUrl"];
            this.logger = logger;

        }
        public async Task OnGet()
        {
            using (HttpClient httpClient = new())
            {
                var responce = await httpClient.GetAsync(channelsUrl);
                if (responce.IsSuccessStatusCode)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<HubDto>>(result);
                }

            }
        }

        public async Task<IActionResult> OnPostLogOff()
        {
            await hubManager.StopAllConnections();
            Console.WriteLine("All connections closed");
            return this.RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostSubscribe(string data)
        {
            if (!hubManager.IsOpened(data))
            {
                await ReadHubInfo(data);
            }
            else
            {
                Console.WriteLine($"Connection \"{data}\" already opened");
            }
            return this.RedirectToPage("/Home");
        }
        public async Task<IActionResult> OnPostUnsubscribe(string data)
        {
            await hubManager.StopConnection(data);
            Console.WriteLine($"Connection \"{data}\" closed");
            return this.RedirectToPage("/Home");
        }

        private async Task ReadHubInfo(string hubName)
        {
            var token = HttpContext.Session.GetString("Token");
            var hubUrl = configuration["GetInfoUrl"] + hubName;
            HubConnection hubConnection = new HubConnectionBuilder().WithUrl(hubUrl, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            }).Build();
            hubManager.AddConnection(hubName, hubConnection);
            hubConnection.StartAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine($"There was an error opening the connection: {task.Exception.GetBaseException()}");
                }
                else
                {

                    Console.WriteLine($"Connection \"{hubName}\" opened");

                    hubConnection.On<object>("Send", Console.WriteLine);

                }
            }).Wait();
        }


    }
}
