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
    
    [HttpGet]
    public async Task<List<Chat>> Get() => await _chatService.GetAsync();
    
    [HttpPost]
    public async Task<IActionResult> Post(Chat newChat)
    {
        await _chatService.CreateAsync(newChat);
        return CreatedAtAction(nameof(Get), new { id = newChat.Id }, newChat);
    }
}