namespace DontThinkJustDrink.Api.Settings
{
    public interface IBasicSecuritySettings
    {
        string MobileAppPassword { get; set; }
    }

    public static class BasicAuthUsernames
    {
        public const string Mobile = "DTJDMobileApp";
    }
}
