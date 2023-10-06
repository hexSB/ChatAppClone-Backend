using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatCloneApi.Hubs;
using ChatCloneApi.Models;
using ChatCloneApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;

namespace ChatCloneApi.Controller;


[ApiController]
[Route("api/[controller]")]
[Authorize]


public class GroupController : ControllerBase
{
    private readonly GroupService _groupService;
    
    
    public GroupController(GroupService groupService)
    {
        _groupService = groupService;

    }
    
    [HttpGet("join/{id:length(24)}")]
    
    public async Task<ActionResult<Group>> Get(string id)
    {

        var group = await _groupService.GetAsync(id);
        
        if (group is null)
        {
            return NotFound();
        }
        var userId = User.Identity.Name;

        if (!group.MembersId.Contains(userId))
        {
            group.MembersId.Add(userId);
            await _groupService.UpdateAsync(id, group);
        }

        else
        {
            return NotFound("Already in group");
        }

        return group;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Group newGroup)
    {
        var userId = User.Identity.Name;
        newGroup.MembersId.Add(userId);
        await _groupService.CreateAsync(newGroup);
        
        return CreatedAtAction(nameof(Get), new { id = newGroup.Id}, newGroup);
    }

    [HttpGet]

    public async Task<List<Group>> Get()
    {
        var name = User.Identity.Name;
        Console.WriteLine("test " + name);
        return await _groupService.GetAsync();
        
    }

    [HttpGet("joined")]
    public async Task<ActionResult<Group>> GetJoined()
    {
        var userId = User.Identity.Name;
        var groups = await _groupService.GetJoined(userId);
        if (groups is null)
        {
            return NotFound();
        }
        return Ok(groups);
    }


}