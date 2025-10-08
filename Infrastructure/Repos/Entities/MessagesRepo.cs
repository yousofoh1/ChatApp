using Core.Interfaces.Repos.Entities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.Entities;



public class MessagesRepo(AppDbContext context, UserManager<AppUser> userManager) : BaseRepo<Message>(context), IMessagesRepo
{
    public async Task<IEnumerable<Message>> GetAllUnreadAsync(string receiverId, CancellationToken cancellationToken = default)
    {
        return await context.Messages.Where(m => m.ReceiverId == receiverId && m.IsRead == false)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }


    public async Task MarkAllAsReadAsync(string receiverId, CancellationToken cancellationToken = default)
    {
        var receiverIdParam = new SqlParameter("@receiverId", receiverId);

        await context.Database.ExecuteSqlRawAsync(
            "exec markAllAsReadAsync @receiverId",
            receiverIdParam);

    }


    public async Task<IEnumerable<Message>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await context.Messages
            .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default)
    {
        await context.Messages.AddAsync(message, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return message;
    }
}
