using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeWebApp.HubsManager
{
    public class HubManager : IHubManager
    {
        public Dictionary<string, HubConnection> hubConnections { get; set; } = new();
        public async Task StopAllConnections()
        {
            foreach (var conn in hubConnections)
            {
                await conn.Value.StopAsync();
                await conn.Value.DisposeAsync();
            }
            hubConnections.Clear();
        }

        public void AddConnection(string hubName, HubConnection hubConnection)
        {
            if (!hubConnections.ContainsKey(hubName))
            {
                hubConnections.Add(hubName, hubConnection);
            }
        }

        public async Task StopConnection(string hubName)
        {
            if (hubConnections.ContainsKey(hubName))
            {
                await hubConnections[hubName].StopAsync();
                await hubConnections[hubName].DisposeAsync();
                hubConnections.Remove(hubName);
            }
        }

        public bool IsOpened(string hubName)
        {
            return hubConnections.ContainsKey(hubName);
        }

    }
}
