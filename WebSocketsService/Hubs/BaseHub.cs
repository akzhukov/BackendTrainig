using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocketsShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebSocketsService.Hubs
{
    [Authorize]
    public abstract class BaseHub : Hub
    {
        public Guid guid { get; } = Guid.NewGuid();
        public abstract HubType hubType { get; }
        public abstract Task Send();
        public abstract Task SendClient(string connectionId);
        protected abstract object CustomizeData(object data, string groupName);
        public override async Task OnConnectedAsync()
        {
            var role = ((ClaimsIdentity)Context.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();
            if (role != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var role = ((ClaimsIdentity)Context.User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();
            if (role != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
