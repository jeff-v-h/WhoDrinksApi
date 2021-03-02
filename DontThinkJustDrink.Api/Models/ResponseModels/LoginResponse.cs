namespace DontThinkJustDrink.Api.Models.ResponseModels
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public bool ShouldUpdatePassword { get; set; }
    }
}
