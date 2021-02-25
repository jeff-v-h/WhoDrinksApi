namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class UserFeedbackRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
    }
}
