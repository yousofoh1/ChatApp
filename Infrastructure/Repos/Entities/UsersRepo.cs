using Core.Dtos.Auth;
using Core.Interfaces.Repos.Entities;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.Entities;



public class UsersRepo(AppDbContext context, UserManager<AppUser> userManager) : BaseRepo<AppUser>(context), IUsersRepo
{
    public async Task<UserRDto> UpdateAsync(UserUDto userUDto, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userUDto.Id) ?? throw new AppException("User not found");
        user.FirstName = userUDto.FirstName ?? user.FirstName;
        user.LastName = userUDto.LastName ?? user.LastName;
        user.Email = userUDto.Email ?? user.Email;
        user.UserName = userUDto.UserName ?? user.UserName;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }

        return user.MapToRDto();
    }

    public async Task UpdateImageAsync(string userId, string imageUrl)
    {

        var user = await userManager.FindByIdAsync(userId) ?? throw new AppException("User not found");
        user.ImageUrl = imageUrl ?? user.ImageUrl;
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }
    }

    public async Task<UserRDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id) ?? throw new AppException("User not found");
        return user.MapToRDto();
    }

    public async Task<IEnumerable<UserRDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        return users.Select(u => u.MapToRDto());
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id) ?? throw new AppException("User not found");
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }
    }
}
