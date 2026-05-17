using PersonalFinance.Shared.DTOs.Profile;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface IProfileService{Task<UserProfileDto> GetProfileAsync(Guid userId);

        Task<UserProfileDto> UpdateProfileAsync( Guid userId,UpdateProfileRequestDto request);
    }
}
