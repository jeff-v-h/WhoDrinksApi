﻿using DontThinkJustDrink.Api.Helpers.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DontThinkJustDrink.Api.Models.Database
{
    public class Deck
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public List<GameTypes> Tags { get; set; }
    }
}
