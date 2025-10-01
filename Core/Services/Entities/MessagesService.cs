using Core.Interfaces;
using Core.Interfaces.Repos.Entities;
using Core.Interfaces.Services.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Entities;

public class MessagesService(IUOW uOW) : IMessagesService
{
    public async Task<IEnumerable<Message>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await uOW.Messages.GetAllAsync(userId, cancellationToken);
    }

    public async Task<IEnumerable<Message>> GetAllUnreadAsync(string receiverId, CancellationToken cancellationToken = default)
    {
        return await uOW.Messages.GetAllUnreadAsync(receiverId, cancellationToken);
    }

    public async Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default)
    {
        return await uOW.Messages.CreateAsync(message, cancellationToken);
    }
}
