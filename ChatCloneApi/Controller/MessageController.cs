using ChatCloneApi.Hubs;
using ChatCloneApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatCloneApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IHubContext<ChatHub> _chatHub;

    public MessageController(IHubContext<ChatHub> chatHub)
    {
        _chatHub= chatHub;
    }

    // [HttpPost]
    // public async Task<IActionResult> Create(Message message)
    // {
    //     await _chatHub.Clients.Group(message.GroupId).SendAsync("ReceiveMessage", message);
    //     return Ok();
    // }
    
    
    
    
}