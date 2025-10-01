using Core.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IUsersService
{
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserRDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserRDto> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<UserRDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<UserRDto> UpdateAsync(UserUDto userUDto, CancellationToken cancellationToken = default);
    Task UpdateImageAsync(string userId, IFormFile imageUrl);
}
