namespace DontThinkJustDrink.Api.Models.ResponseModels
{
    public class LoginResponse
    {
        public bool IsVerified { get; set; }
        public string Username { get; set; }
        public bool ShouldUpdatePassword { get; set; }
    }
}
