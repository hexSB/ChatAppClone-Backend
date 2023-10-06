using ChatCloneApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ChatCloneApi.Services;

public class ChatService
{
    private readonly IMongoCollection<Chat> _chatCollection;
    
    public ChatService(IOptions<ChatCloneDatabaseSettings> chatCloneDatabaseSettings)
    {
        
        var mongoClient = new MongoClient(chatCloneDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(chatCloneDatabaseSettings.Value.DatabaseName);
        
        _chatCollection = mongoDatabase.GetCollection<Chat>(chatCloneDatabaseSettings.Value.ChatsCollectionName);
    }
    
    public async Task<List<Chat>> GetAsync() => await _chatCollection.Find(chat => true).ToListAsync();
    
    public async Task CreateAsync(Chat newChat) => await _chatCollection.InsertOneAsync(newChat);
}