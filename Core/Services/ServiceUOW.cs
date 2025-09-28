using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceUOW(IUOW uow) : IServicesUOW
    {
        private Lazy<IAuthService> _authService
            = new(() => new AuthService(uow));
        public IAuthService AuthService => _authService.Value;
    }
}
