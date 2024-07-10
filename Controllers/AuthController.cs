using AuthApi.Models;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.AuthenticateAsync(request);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        return Ok(new { Message = "Login successful", User = user });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        await _userService.CreateUserAsync(user);
        return Ok(new { Message = "Registration successful" });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
