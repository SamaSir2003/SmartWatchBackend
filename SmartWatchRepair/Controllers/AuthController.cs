using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartWatchRepair.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(RepairServiceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Validate user credentials
            var user = _context.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user.Id, user.Role);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string userId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),  // Set token expiry
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}