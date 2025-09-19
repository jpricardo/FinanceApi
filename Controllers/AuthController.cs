using FinanceApi.Data;
using FinanceApi.Models.DTOs.Auth;
using FinanceApi.Models.Entities;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly TokenService _tokenService;

        public AuthController(UserContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegister)
        {
            if (await _context.Users.AnyAsync(u => u.Username == userRegister.Username))
            {
                return BadRequest("Email already in use.");
            }

            var (hash, salt) = PasswordService.HashPassword(userRegister.Password);

            var user = new User
            {
                Username = userRegister.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username);

            if (user == null || !PasswordService.VerifyPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize] // Protect this endpoint
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(int.Parse(userIdString));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { user.Id, user.Username, });
        }
    }
}