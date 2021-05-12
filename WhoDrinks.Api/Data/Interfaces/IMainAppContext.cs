using WhoDrinks.Api.Models.Database;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Data.Interfaces
{
    public interface IMainAppContext
    {
        IMongoCollection<AppVersion> AppVersions { get; }
        IMongoCollection<UserFeedback> UsersFeedback { get; }
        IMongoCollection<User> Users { get; }
        IMongoCollection<UserCredentials> UsersCredentials { get; }
        IMongoCollection<Deck> Decks { get; }
        Task<IClientSessionHandle> StartSessionAsync();
    }
}
