using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PersonalFinance.API.Exceptions;
namespace PersonalFinance.API.Services.Implementations
{
    public class AuthService: IAuthService
    {
        public readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<AuthResponseDto> RegisterAsync (RegisterRequestDto request)
        {
            //1. Check if email exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
                throw new AppException("Email already exists");

            //Check constraints
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new Exception("Password is required");

            //2 . Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            //3.Create user

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                SurName = request.SurName,
                Email = request.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            //4. Save

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var token = GenerateToken(user);


            //5 Return response

            return new AuthResponseDto {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name   ,
                Token = token
            };
            
                
            
        }

        public async Task <AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            //1 .Find user
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            //2 .Verify password
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid  || user is null ) throw new AppException("Email or password is incorrect");

            //3 return response
            var token = GenerateToken(user);
            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = token
                
            };
        }

        //Generate Tokens

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            
            var creds  = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires:DateTime.UtcNow.AddMinutes(60),
                signingCredentials:creds
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
                
        }
            
    }
}
