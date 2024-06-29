using System.Globalization;
using ChatCloneApi.Models;
using ChatCloneApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatCloneApi.Controller;

[ApiController]
[Route("api/[controller]")]


public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;
    
    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }
    

    
    [HttpGet("id/{id:length(24)}")]
    public async Task<ActionResult<Chat>> GetById(string id)
    {
        var message = await _chatService.GetByIdAsync(id);

        if (message == null)
        {
            return NotFound();
        }

        return Ok(message);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Chat message)
    {
        var date = DateTime.Now;
        var formattedDate = date.ToString("MM-dd-yyyy HH:mm");
        message.Timestamp = formattedDate;
        await _chatService.CreateAsync(message);
        return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
    }
}