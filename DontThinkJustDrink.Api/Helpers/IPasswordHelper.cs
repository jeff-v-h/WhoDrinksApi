namespace DontThinkJustDrink.Api.Helpers
{
    public interface IPasswordHelper
    {
        (string, string) GetPasswordSaltAndHash(string pw);
    }
}
