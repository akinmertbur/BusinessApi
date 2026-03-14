using BusinessApi.Data;
using BusinessApi.DTOs.Auth;
using BusinessApi.Entities;
using BusinessApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ITokenService tokenService, ILogger<AuthController> logger) {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserReadDto>> Register(RegisterUserDto dto) {
            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists) {
                return BadRequest(new { message = "Email already exists." });
            }

            var user = new User {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = new UserReadDto {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(nameof(Register), result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(LoginUserDto dto) {
            _logger.LogInformation("Login attempt for {Email}", dto.Email);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) {
                _logger.LogWarning("Failed login attempt for {Email}", dto.Email);
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = _tokenService.CreateToken(user);

            return Ok(new {
                token
            });
        }
    }
}
