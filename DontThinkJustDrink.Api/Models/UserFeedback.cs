using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DontThinkJustDrink.Api.Models
{
    public class UserFeedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
    }
}
