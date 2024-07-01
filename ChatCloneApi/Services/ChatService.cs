using ChatCloneApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ChatCloneApi.Services;

public class ChatService
{
    private readonly IMongoCollection<Chat> _chatCollection;
    
    public ChatService(IOptions<ChatCloneDatabaseSettings> chatCloneDatabaseSettings)
    {
        MongoClient mongoClient;
        string connectSecret = Environment.GetEnvironmentVariable("Connect_Secret");

        if (string.IsNullOrEmpty(connectSecret))
        {
            mongoClient = new MongoClient(chatCloneDatabaseSettings.Value.ConnectionString);
        }
        else
        {
            mongoClient = new MongoClient(connectSecret);
        }

        var mongoDatabase = mongoClient.GetDatabase(chatCloneDatabaseSettings.Value.DatabaseName);
        _chatCollection = mongoDatabase.GetCollection<Chat>(chatCloneDatabaseSettings.Value.MessagesCollectionName);
    }
    
    public async Task<List<Chat>> GetAsync() => await _chatCollection.Find(chat => true).ToListAsync();
    
    public async Task CreateAsync(Chat newMessage) => await _chatCollection.InsertOneAsync(newMessage);
    
    public async Task<List<Chat>> GetByIdAsync(string groupID)
    {
        return await _chatCollection.Find(chat => chat.GroupId == groupID).ToListAsync();
    }
}


    





