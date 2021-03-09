using DontThinkJustDrink.Api.Settings.Interfaces;

namespace DontThinkJustDrink.Api.Settings
{
    public class HashingSettings : IHashingSettings
    {
        public int Iterations { get; set; }
    }
}
