using PersonalFinance.Shared.DTOs.Auth;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task ChangePasswordAsync(Guid userId,ChangePasswordRequestDto request);
    }
}
