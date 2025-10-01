using Core.Dtos.Auth;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Entities;

public class UsersService(IUOW uow) : IUsersService
{
    public Task<UserRDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserRDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await uow.Users.GetByUserNameAsync(userName, cancellationToken);
    }

    public async Task<UserRDto> UpdateAsync(UserUDto userUDto, CancellationToken cancellationToken = default)
    {
        return await uow.Users.UpdateAsync(userUDto, cancellationToken);
    }

    public async Task UpdateImageAsync(string userId, IFormFile image)
    {
        string imageUrl = await FilesService.SaveFileAsync(image, uow.Host, uow.HttpContext);
        await uow.Users.UpdateImageAsync(userId, imageUrl);

    }


    public Task<IEnumerable<UserRDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = uow.Users.GetAllAsync(cancellationToken);
        return users;
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


}
