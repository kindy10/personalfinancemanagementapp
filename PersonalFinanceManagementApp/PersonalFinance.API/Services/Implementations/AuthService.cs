using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Auth;

namespace PersonalFinance.API.Services.Implementations
{
    public class AuthService: IAuthService
    {
        public readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AuthResponseDto> RegisterAsync (RegisterRequestDto request)
        {
            //1. Check if email exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
                throw new Exception("Email already exists");

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


            //5 Return response

            return new AuthResponseDto {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name   ,
                Token = " " // later (JWT)
            };
            
                
            
        }

        public async Task <AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            //1 .Find user
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null) throw new Exception("Invalid credentials");

            //2 .Verify password
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid) throw new Exception("Invalid credentials");

            //3 return response

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = ""  // later
            };
        }
            
    }
}
