using AuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(LoginRequest request)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
