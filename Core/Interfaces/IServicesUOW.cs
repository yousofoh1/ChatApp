using Core.Interfaces.Services;
using Core.Interfaces.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServicesUOW
    {
        IAuthService Auth { get; }
        IUsersService Users { get; }
    }
}
