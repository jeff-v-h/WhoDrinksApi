using System;

namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class UserFeedbackRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DeviceId { get; set; }
        public string Feedback { get; set; }
        public DateTime UserCreatedOn { get; set; } 
    }
}
