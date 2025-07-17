using Microsoft.AspNetCore.SignalR;

public class SimpleHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}