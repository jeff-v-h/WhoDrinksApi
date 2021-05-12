using WhoDrinks.Api.Settings.Interfaces;

namespace WhoDrinks.Api.Settings
{
    public class BasicSecuritySettings : IBasicSecuritySettings
    {
        public string MobileAppPassword { get; set; }
    }
}
