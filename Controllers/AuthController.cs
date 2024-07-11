using AuthApi.Models;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public AuthController(IUserService userService, INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            await _notificationService.AddNotificationAsync(user.Id, "User has logged in");

            return Ok(new { Message = "Login successful", User = user });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _userService.CreateUserAsync(user);
            return Ok(new { Message = "Registration successful", User = user });
        }

        [HttpPost("checkNotification")]
        public async Task<IActionResult> CheckNotification([FromBody] int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            var messages = await _notificationService.GetNotificationAsync(user.Id);
            
            await _notificationService.AddNotificationAsync(user.Id, $"User has checked notification at {DateTime.Now}");

            return Ok(new { Message = $@"Check Notifications successful. _@Messages: _@{MessagesToString(messages)}", User = user });
        }

        private string MessagesToString(List<string> messages)
        {
            if (messages.Count() == 0) return "Empty";

            var str = string.Empty;
            foreach (var message in messages)
            {
                str += message.ToString() + "_@";
            }
            return str;
        }
    }
}