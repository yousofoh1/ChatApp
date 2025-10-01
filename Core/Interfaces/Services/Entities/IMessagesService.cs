using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Entities;

public interface IMessagesService
{
    Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default);
    Task<IEnumerable<Message>> GetAllAsync(string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Message>> GetAllUnreadAsync(string receiverId, CancellationToken cancellationToken = default);
}
