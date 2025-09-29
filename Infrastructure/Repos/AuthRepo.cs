using Core.Dtos.Auth;
using Core.Dtos.Requests;
using Core.Interfaces.Repos;
using Domain.Exceptions;
using Infrastructure.Data;
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
        public async Task<UserRDto> CheckPasswordAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new AppException("Invalid email or password");

            if (await userManager.CheckPasswordAsync(user, password)) return user.MapToRDto();
            else throw new AppException("Invalid email or password");

        }

        public async Task<UserRDto> RegisterAsync(RegisterRequest registerRequest)
        {
            // Implement registration logic here
            var user = new AppUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName
            };


            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }



            return user.MapToRDto();

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
