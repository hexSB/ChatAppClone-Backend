using ChatCloneApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatCloneApi.Hubs;


public class ChatHub : Hub
{
    
    private readonly string _botUser;
    private readonly IDictionary<string, UserConnection> _connections;

    public ChatHub(IDictionary<string, UserConnection> connections)
    {
        _botUser = "ChatBot";
        _connections = connections;
    }
    public async Task JoinRoom(UserConnection userConnection)
    
    {


        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.groupId);
        
        _connections[Context.ConnectionId] = userConnection;
        
        
        await Clients.Group(userConnection.groupId).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has joined the room {userConnection.groupId}");
    }
    
    public async Task Leave(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("ReceiveMessage", _botUser, $"{Context.ConnectionId} has left the room {groupId}");
    }

    public async Task SendGroupUpdate()
    {
        await Clients.All.SendAsync("ReceiveGroupUpdate");
    }

    public async Task SendMessage(string message)
    {
        if(_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            await Clients.Group(userConnection.groupId).SendAsync("ReceiveMessage", userConnection.User, message);
        }
        
    }
}