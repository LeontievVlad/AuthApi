using AuthApi.Models;

namespace AuthApi.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(LoginRequest request);
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
    }
}
