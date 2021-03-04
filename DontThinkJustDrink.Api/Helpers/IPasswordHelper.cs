namespace DontThinkJustDrink.Api.Helpers
{
    public interface IPasswordHelper
    {
        string Hash(string pw);
        (bool verified, bool needsUpgrade) Check(string storedHash, string password);
    }
}
