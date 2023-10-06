using ChatCloneApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatCloneApi.Services;

public class GroupService
{
    private readonly IMongoCollection<Group> _groupCollection;
    
    public GroupService(IOptions<ChatCloneDatabaseSettings> chatCloneDatabaseSettings)
    {
        
        var mongoClient = new MongoClient(chatCloneDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(chatCloneDatabaseSettings.Value.DatabaseName);
        
        _groupCollection = mongoDatabase.GetCollection<Group>(chatCloneDatabaseSettings.Value.GroupChatCollectionName);
    }
    
    
    public async Task UpdateAsync(string id, Group updateGroup) => await _groupCollection.ReplaceOneAsync(group => group.Id == id, updateGroup);
    
    public async Task CreateAsync(Group newGroup) => await _groupCollection.InsertOneAsync(newGroup);
    
    public async Task<List<Group>> GetAsync() => await _groupCollection.Find(group => true).ToListAsync();
    
    public async Task<Group?> GetAsync(string id) => await _groupCollection.Find(group => group.Id == id).FirstOrDefaultAsync();
    
    public async Task<List<Group?>> GetJoined(string userId) => await _groupCollection.Find(group => group.MembersId.Contains(userId)).ToListAsync();
    
    
}