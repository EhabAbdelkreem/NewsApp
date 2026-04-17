using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Application.DTOs;
using NewsApp.Domain.Entities;
using NewsApp.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration config, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                return Unauthorized("بيانات الدخول غير صحيحة");

            var token = GenerateJwtToken(user);
            return Ok(new AuthResponseDto(token, user.FullName, user.Role));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await _unitOfWork.Users.GetByEmailAsync(request.Email) != null)
                return BadRequest("البريد الإلكتروني مستخدم");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = "User"
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new { message = "تم التسجيل بنجاح" });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(7), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}