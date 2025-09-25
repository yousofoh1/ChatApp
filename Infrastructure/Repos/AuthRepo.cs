using Core.Contracts.Repos;
using Core.Dtos.Requests;
using Domain.Contracts;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class AuthRepo(UserManager<AppUser> userManager) : IAuthRepo
    {
        public async Task<LoginRDto> LoginAsync(LoginRequest loginRequest)
        {
            // Implement login logic here
            var user = await userManager.FindByEmailAsync(loginRequest.Email) ?? throw new Exception();

            bool isPasswordValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPasswordValid) throw new Exception();

            return new LoginRDto(
                user.Id,
                user.UserName ?? "",
                user.Email ?? "",
                GenerateJwtToken(user.Id, user.UserName, user.Email)
            );
        }

        public async Task<LoginRDto> RegisterAsync(RegisterRequest registerRequest)
        {
            // Implement registration logic here
            var user = new AppUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };


            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }



            return new LoginRDto(
                user.Id,
                user.UserName ?? "",
                user.Email ?? "",
                GenerateJwtToken(user.Id, user.UserName, user.Email)
            );

        }

        public string GenerateJwtToken(string userId, string username, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yousofo-secret-code-yousofo-secret-code"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yousofo",
                audience: "yousofo_audience",
                claims: [
                    new Claim("userId", userId),
                    new Claim("username", username),
                    new Claim("email", email)
                ],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            // Implement refresh token generation logic here
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateRefreshToken(string userId, string refreshToken)
        {
            // Implement refresh token validation logic here
            throw new NotImplementedException();
        }
    }
}
