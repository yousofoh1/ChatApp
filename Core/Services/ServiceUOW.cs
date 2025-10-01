using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Entities;
using Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceUOW(IUOW uow) : IServicesUOW
    {
        private Lazy<IAuthService> _auth
            = new(() => new AuthService(uow));
        private Lazy<IUsersService> _users
            = new(() => new UsersService(uow));
        private Lazy<IMessagesService> _messages
            = new(() => new MessagesService(uow));
        public IAuthService Auth => _auth.Value;
        public IUsersService Users => _users.Value;
        public IMessagesService Messages => _messages.Value;
    }
}
