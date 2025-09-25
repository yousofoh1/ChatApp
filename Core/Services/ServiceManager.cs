using Core.Contracts;
using Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceManager(IUOW uow) : IServiceManager
    {
        private Lazy<IAuthService> _authService
            = new(() => new AuthService(uow));
        public IAuthService AuthService => _authService.Value;
    }
}
