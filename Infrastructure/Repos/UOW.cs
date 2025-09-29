using Core.Interfaces;
using Core.Interfaces.Repos;
using Core.Interfaces.Repos.Entities;
using Infrastructure.Data;
using Infrastructure.Repos.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class UOW(AppDbContext context, UserManager<AppUser> userManager, IHostEnvironment _host, IHttpContextAccessor _http) : IUOW
    {
        public IHostEnvironment Host => _host;
        public IHttpContextAccessor HttpContext => _http;

        private readonly Lazy<AuthRepo> _auth = new(() => new(userManager));
        private readonly Lazy<UsersRepo> _users = new(() => new(context, userManager));

        public IAuthRepo Auth => _auth.Value;
        public IUsersRepo Users => _users.Value;
    }
}
