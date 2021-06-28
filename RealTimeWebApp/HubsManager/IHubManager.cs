using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeWebApp.HubsManager
{
    public interface IHubManager
    {
        Task StopAllConnections();
        void AddConnection(string hubName, HubConnection hubConnection);
        Task StopConnection(string hubName);
        bool IsOpened(string hubName);
    }
}
