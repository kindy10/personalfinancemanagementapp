using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Profile;

namespace PersonalFinance.Mobile.Services;

public class ProfileService
{
    private readonly ApiService _apiService;

    public ProfileService()
    {
        _apiService = new ApiService();
    }

    // Get current user profile
    public async Task<UserProfileDto>
        GetProfileAsync()
    {
        var response = await _apiService.GetAsync<ApiResponse<UserProfileDto>>("profile");

        return response.Data;
    }

    // Update profile
    public async Task UpdateProfileAsync(
        UpdateProfileRequestDto request)
    {
        await _apiService.PutAsync( "profile",request);
    }
}