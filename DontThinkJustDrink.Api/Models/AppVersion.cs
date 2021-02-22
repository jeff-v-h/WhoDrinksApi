using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DontThinkJustDrink.Api.Models
{
    public class AppVersion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Version { get; set; }
        public string ForceUpgrade { get; set; }
        public string RecommendUpgrade { get; set; }
    }
}
