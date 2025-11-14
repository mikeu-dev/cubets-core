using cubets_core.Modules.Auth.DTOs;
using CubetsCore.Modules.Auth.DTOs;

namespace cubets_core.Modules.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task<AuthResponseDto> GuestLoginAsync();

        Task<LogoutResponseDto> LogoutAsync(RevokedTokenRequestDto dto);
    }
}
