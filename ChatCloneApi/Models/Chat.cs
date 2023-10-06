﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatCloneApi.Models;

public class Chat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Message { get; set; } = null!;

    public string Author { get; set; } = null;

    public int Time { get; set; }
}