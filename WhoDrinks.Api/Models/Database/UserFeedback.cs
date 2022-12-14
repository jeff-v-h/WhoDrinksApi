using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WhoDrinks.Api.Models.Database
{
    public class UserFeedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DeviceId { get; set; }
        public string Feedback { get; set; }
        public DateTime UserCreatedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
