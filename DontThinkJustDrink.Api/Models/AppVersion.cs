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
        public bool ForceUpgrade { get; set; }
        public bool RecommendUpgrade { get; set; }
        public string Announcement { get; set; }
        // FE or BE prefix = frontend or backend version
        public string AssociatedPrivacyPolicyVersion { get; set; }
        public string AssociatedTCsVersion { get; set; }
        public bool ForceNewTCsAgreement { get; set; }
        public bool ForceNewPrivacyPolicyAgreement { get; set; }
        // These should only be used by frontend if forcing new agreements since frontend
        // should already have the originals on load to prevent need for network connection
        public string TermsAndConditions { get; set; }
        public string PrivacyPolicy { get; set; }
    }
}
