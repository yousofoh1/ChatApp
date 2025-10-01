using Core.Interfaces.Repos;
using Core.Interfaces.Repos.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using static System.Net.WebRequestMethods;

namespace Core.Interfaces
{
    public interface IUOW
    {
        IAuthRepo Auth { get; }
        IUsersRepo Users { get; }
        IMessagesRepo Messages { get; }
        IHostEnvironment Host { get; }
        IHttpContextAccessor HttpContext { get; }
    }
}