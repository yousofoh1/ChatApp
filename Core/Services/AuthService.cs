using Core.Dtos.Auth;
using Core.Dtos.Requests;
using Core.Interfaces;
using Core.Interfaces.Services.Entities;
using Core.Services.Common;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService(IUOW uow) : IAuthService
    {
        public async Task<LoginSuccess> LoginAsync(LoginRequest loginRequest)
        {
            var user = await uow.Auth.CheckPasswordAsync(loginRequest.Email, loginRequest.Password);

            var token = GenerateJwtToken(user.Id, user.Email);

            return new LoginSuccess(user, token);

        }
        public async Task<LoginSuccess> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = await uow.Auth.RegisterAsync(registerRequest);
            string imageUrl = await FilesService.SaveFileAsync(registerRequest.Image, uow.Host, uow.HttpContext);
            await uow.Users.UpdateImageAsync(user.Id, imageUrl);
            user.ImageUrl = imageUrl;
            var token = GenerateJwtToken(user.Id, user.Email);
            return new LoginSuccess(user, token);
        }
        public string GenerateJwtToken(string userId, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yousofo-secret-code-yousofo-secret-code"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "yousofo",
                Audience = "yousofo_audience",
                Claims = new Dictionary<string, object> {
                    {"userId", userId },
                    {"email", email },
                },
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = creds
            };
            return new JsonWebTokenHandler().CreateToken(descriptor);
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
