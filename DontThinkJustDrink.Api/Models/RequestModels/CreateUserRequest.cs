namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string DeviceId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ConfirmedDisclaimer { get; set; }
        //public bool AgreedToPrivacyPolicy { get; set; }
        //public bool AgreedToTCs { get; set; }
        //public string PrivacyPolicyVersionAgreedTo { get; set; }
        //public string TCsVersionAgreedTo { get; set; }
        public string CurrentAppVersion { get; set; }
    }
}
