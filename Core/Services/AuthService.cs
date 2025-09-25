using Core.Contracts;
using Core.Contracts.Services;
using Core.Dtos.Requests;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService(IUOW uow) : IAuthService
    {
        public async Task<LoginRDto> LoginAsync(LoginRequest loginRequest)
        {
            var loginRDto = await uow.AuthRepo.LoginAsync(loginRequest);
            return loginRDto;
        }
        public string GenerateJwtToken(string userId, string userName)
        {
            throw new NotImplementedException();
        }
        public async Task<LoginRDto> RegisterAsync(RegisterRequest registerRequest)
        {
            var loginRDto = await uow.AuthRepo.RegisterAsync(registerRequest);
            return loginRDto;
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }



        public Task<bool> ValidateRefreshToken(string userId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
