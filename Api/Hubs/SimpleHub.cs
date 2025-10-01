using Core.Dtos.Chat;
using Core.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.Json;

//public class ConnectionInfo
//{
//    public string ConnectionId { get; set; } = null!;
//    public string UserId { get; set; } = null!;
//}
[Authorize]
public class SimpleHub(IServicesUOW services) : Hub
{
    // Track all active connections (optional, helpful for debugging / room logic)
    private static readonly ConcurrentDictionary<string, string> _connections = new();


    public override async Task OnConnectedAsync()
    {
        var cid = Context.ConnectionId;
        var userName = Context.User?.FindFirst("userName")?.Value;
        var userId = Context.User?.FindFirst("userId")?.Value;

        //var messages = await services.Messages.GetAllUnreadAsync(userId);

        if (userName is not null)
        {
            _connections[userName] = cid;
        }

        //await Clients.Caller.SendAsync("UnreadMessages", messages);
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(MessageDto messageDto)
    {
        var receiver = await services.Users.GetByUserNameAsync(messageDto.ReceiverUserName);
        var senderImageUrl = Context.User?.FindFirst("imageUrl")?.Value ?? "";
        var senderUserName = Context.User?.FindFirst("userName")?.Value ?? "Anonymous";
        var senderFullName = Context.User?.FindFirst("senderFullName")?.Value ?? "Anonymous";

        var message = new Message
        {
            ReceiverId = receiver?.Id ?? "",
            SenderId = Context.User?.FindFirst("userId")?.Value ?? "",
            Content = messageDto.Text
        };

        await services.Messages.CreateAsync(message);

        if (receiver == null)
            throw new AppException("User not found");

        _connections.TryGetValue(messageDto.ReceiverUserName,out string? receiverCId);
        Console.WriteLine($"Send messageDto from {senderUserName} to {messageDto.ReceiverUserName}, rcid: {receiverCId}, messageDto: {messageDto.Text}");

        if (receiverCId is not null)
        {
            await Clients.Client(receiverCId).SendAsync("ReceiveMessage", new MessageDto(
                SenderUserName: senderUserName,
                SenderImageUrl: senderImageUrl,
                SenderFullName: senderFullName,
                ReceiverUserName: messageDto.ReceiverUserName,
                Text: messageDto.Text
            ), messageDto);
        }
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var cid = Context.ConnectionId;
        var userName = Context.User?.FindFirst("userName")?.Value;
        if (userName is not null)
        {
            _connections.TryRemove(userName, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }

    

    


}