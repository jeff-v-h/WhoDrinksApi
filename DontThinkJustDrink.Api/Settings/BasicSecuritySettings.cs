using DontThinkJustDrink.Api.Settings.Interfaces;

namespace DontThinkJustDrink.Api.Settings
{
    public class BasicSecuritySettings : IBasicSecuritySettings
    {
        public string MobileAppPassword { get; set; }
    }
}
