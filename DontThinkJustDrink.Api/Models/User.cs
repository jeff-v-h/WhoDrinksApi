using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DontThinkJustDrink.Api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool AgreedToPrivacyPolicy { get; set; }
        public bool AgreedToTCs { get; set; }
        // FE or BE prefix = frontend or backend version
        public string PrivacyPolicyVersionAgreedTo { get; set; }
        public string TCsVersionAgreedTo { get; set; }
        public string CurrentAppVersion { get; set; }
    }
}
