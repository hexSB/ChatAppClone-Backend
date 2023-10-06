using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatCloneApi.Models;

public class Group
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }
    
    public string GroupName { get; set; } = null!;

    public List<string> MembersId { get; set; } = new List<string>();
    
    
}