using DataProviders;
using DataProviders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocketsShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketsService.Hubs
{
    [Authorize]
    public class LinePageChartHub : BaseHub
    {
        private readonly IDataProvider provider;
        public override HubType hubType => HubType.LinePageChart;
        public LinePageChartHub(IDataProviderFactory dataProviderFactory)
        {
            provider = dataProviderFactory.GetProvider(hubType);
        }

        public override async Task Send()
        {
            var data = await provider.GetData();
            if (Clients != null)
                await Clients.All.SendAsync("Send", data);
        }

        public override async Task SendClient(string connectionId)
        {
            if (String.IsNullOrEmpty(connectionId))
            {
                return;
            }
            var data = provider.GetData();
            if (Clients != null)
            {
                await Clients.Client(connectionId).SendAsync("Send", data);
            }
        }


        protected override object CustomizeData(object data, string groupNames)
        {
            throw new NotImplementedException();
        }
    }
}
