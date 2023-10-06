using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatCloneApi.Models;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string Id { get; set; } = null!;
    public string GroupId { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}