using Core.Dtos.Chat;
using Core.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

//public class ConnectionInfo
//{
//    public string ConnectionId { get; set; } = null!;
//    public string UserId { get; set; } = null!;
//}

public class SimpleHub(IServicesUOW services) : Hub
{
    // Track all active connections (optional, helpful for debugging / room logic)
    private static readonly ConcurrentDictionary<string, string> _connections = new();


    public override async Task OnConnectedAsync()
    {
        var cid = Context.ConnectionId;
        _connections[cid] = "";

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(MessageDto message)
    {
        var receiver = await services.Users.GetByIdAsync(message.ReceiverId);

        if (receiver == null)
            throw new AppException("User not found");

        string receiverCId = _connections.FirstOrDefault(c => c.Value == message.ReceiverId).Key;


        await Clients.Client(message.ReceiverId).SendAsync(new MessageDto
        (
            SenderId:message.SenderId,
            ReceiverId: message.ReceiverId,
            ReceiverImageUrl:receiver.ImageUrl,
            Text: message.Text
        ).ToString(), message);
    }
}