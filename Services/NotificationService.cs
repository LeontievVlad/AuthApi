using System;
using System.Text.Json;
using System.Threading.Tasks;
using AuthApi.Models;
using Microsoft.AspNetCore.SignalR;
using AuthApi.Hubs;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task AddNotificationAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                CreatedAt = DateTime.Now,
                IsProcessed = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", userId, message);
        }


        public async Task<List<string>> GetNotificationAsync(int userId)
        {
            return await _context.Notifications
                .Where(x => x.UserId == userId && x.IsProcessed == false)
                .Select(x=>x.Message)
                .ToListAsync();
        }
        public async Task ProcessNotificationsAsync()
        {
            var notifications = await _context.Notifications
                .Where(n => !n.IsProcessed)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                Console.WriteLine($"Processing notification for user {notification.UserId}: {notification.Message}");
                
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.UserId, notification.Message);

                notification.IsProcessed = true;
                _context.Notifications.Update(notification);
            }

            await _context.SaveChangesAsync();
        }
    }
}
