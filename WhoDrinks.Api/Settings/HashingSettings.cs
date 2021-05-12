using WhoDrinks.Api.Settings.Interfaces;

namespace WhoDrinks.Api.Settings
{
    public class HashingSettings : IHashingSettings
    {
        public int Iterations { get; set; }
    }
}
