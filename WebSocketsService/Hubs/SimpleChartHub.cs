using AutoMapper;
using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SocketsShared.DBContext;
using SocketsShared.DTOs;
using SocketsShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketsService.Hubs
{
    [Authorize]
    public class SimpleChartHub : BaseHub
    {
        private IDataProvider provider;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public override HubType hubType => HubType.SimpleChart;

        public SimpleChartHub(IDataProviderFactory dataProviderFactory, IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            provider = dataProviderFactory.GetProvider(hubType);
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }


        public override async Task Send()
        {
            var data = await provider.GetData();
            using (var scope = scopeFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = roleManager.Roles.Select(x => x.Name).ToList();
                foreach (var group in roles)
                {
                    var customizedData = CustomizeData(data, group);
                    if (Clients != null)
                    {
                        await Clients.Group(group).SendAsync("Send", customizedData);
                    }
                }
            }
        }

        public override async Task SendClient(string connectionId)
        {
            if (String.IsNullOrEmpty(connectionId))
            {
                return;
            }
            var data = await provider.GetData();
            if (Clients != null)
            {
                await Clients.Client(connectionId).SendAsync("Send", data);
            }
        }

        protected override object CustomizeData(object data, string groupName)
        {
            var sc = data as SimpleChart;
            if (sc == null)
            {
                return data;
            }

            if (groupName == "Admin")
            {
                return mapper.Map<SimpleChartAdminDto>(sc);
            }

            return mapper.Map<SimpleChartOthersDto>(sc);
        }
    }
}
