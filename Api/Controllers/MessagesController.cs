using Core.Dtos.Auth;
using Core.Dtos.Chat;
using Core.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class MessagesController(IServicesUOW services) : BaseController
{
    [HttpGet("GetAllUnread/{userId}")]
    public async Task<IEnumerable<Message>> GetAllUnread([FromRoute] string userId)
    {
        return await services.Messages.GetAllUnreadAsync(userId);
    }


    [HttpPost("MarkAllAsRead/{userId}")]
    public async Task MarkAllAsRead(string userId)
    {
        await services.Messages.MarkAllAsReadAsync(userId);
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
