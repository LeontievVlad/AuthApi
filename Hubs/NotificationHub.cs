﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AuthApi.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", user, message);
        }
    }
}
