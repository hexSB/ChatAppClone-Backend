namespace ChatCloneApi.Models;

public class ChatCloneDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    
    public string DatabaseName { get; set; } = null!;
    
    public string ChatsCollectionName { get; set; } = null!;
    
    public string GroupChatCollectionName { get; set; } = null!;
}