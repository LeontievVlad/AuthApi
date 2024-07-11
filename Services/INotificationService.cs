using AuthApi.Models;

namespace AuthApi.Services
{
    public interface INotificationService
    {
        Task AddNotificationAsync(int userId, string message);
        Task<List<string>> GetNotificationAsync(int userId);
        Task ProcessNotificationsAsync();
    }
}
