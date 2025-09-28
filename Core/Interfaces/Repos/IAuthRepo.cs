using Core.Dtos.Auth;
using Core.Dtos.Requests;

namespace Core.Interfaces.Repos;

public interface IAuthRepo
{
    string GenerateRefreshToken();
    Task<UserRDto> CheckPasswordAsync(string email, string password);
    Task<UserRDto> RegisterAsync(RegisterRequest user);
    Task<bool> ValidateRefreshToken(string userId, string refreshToken);
}