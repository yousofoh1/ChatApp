using Core.Dtos.Requests;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IAuthService
    {
        Task<LoginRDto> LoginAsync(LoginRequest loginRequest);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
        string GenerateJwtToken(string userId, string userName);
        string GenerateRefreshToken();
        Task<bool> ValidateRefreshToken(string userId, string refreshToken);
        Task<LoginRDto> RegisterAsync(RegisterRequest registerRequest);
    }
}
