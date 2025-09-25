using Core.Dtos.Requests;
using Domain.Contracts;

namespace Core.Contracts.Repos
{
    public interface IAuthRepo
    {
        string GenerateJwtToken(string userId, string username, string email);
        string GenerateRefreshToken();
        Task<LoginRDto> LoginAsync(LoginRequest loginRequest);
        Task<LoginRDto> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> ValidateRefreshToken(string userId, string refreshToken);
    }
}