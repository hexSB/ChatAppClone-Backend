using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace ChatCloneApi.Models;

public class Chat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }
    public string GroupId { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? Timestamp { get; set; }
}


