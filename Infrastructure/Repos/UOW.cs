using Core.Contracts;
using Core.Contracts.Repos;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class UOW(UserManager<AppUser> userManager) : IUOW
    {
        private readonly Lazy<AuthRepo> _authRepo = new(() => new(userManager));
        public IAuthRepo AuthRepo => _authRepo.Value;
    }
}
