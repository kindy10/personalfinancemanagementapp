using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Exceptions;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Profile;

namespace PersonalFinance.API.Services.Implementations;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _context;

    public ProfileService(AppDbContext context)
    {
        _context = context;
    }

    // Get current user profile
    public async Task<UserProfileDto>GetProfileAsync(Guid userId)
    {
        var user = await _context.Users .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new AppException(
                "User not found");

        return new UserProfileDto
        {
            Name = user.Name,
            SurName = user.SurName,
            Email = user.Email
        };
    }

    // Update profile
    public async Task<UserProfileDto>  UpdateProfileAsync( Guid userId, UpdateProfileRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new AppException(
                "User not found");

        // Update fields
        user.Name = request.Name;

        user.SurName = request.SurName;

        user.Email = request.Email;

        await _context.SaveChangesAsync();

        return new UserProfileDto
        {
            Name = user.Name,
            SurName = user.SurName,
            Email = user.Email
        };
    }
}