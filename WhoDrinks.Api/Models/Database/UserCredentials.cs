using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhoDrinks.Api.Models.Database
{
    public class UserCredentials
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Hashed { get; set; }
    }
}
