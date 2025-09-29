using Core.Dtos.Auth;
using Core.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Entities
{
    public interface IAuthService
    {
        Task<LoginSuccess> LoginAsync(LoginRequest loginRequest);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
        string GenerateJwtToken(string userId, string userName);
        string GenerateRefreshToken();
        Task<bool> ValidateRefreshToken(string userId, string refreshToken);
        Task<LoginSuccess> RegisterAsync(RegisterRequest registerRequest);
    }
}
