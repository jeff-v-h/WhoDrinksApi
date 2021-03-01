using DontThinkJustDrink.Api.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Data.Interfaces
{
    public interface IMainAppContext
    {
        IMongoCollection<AppVersion> AppVersions { get; }
        IMongoCollection<UserFeedback> UsersFeedback { get; }
        IMongoCollection<User> Users { get; }
        IMongoCollection<UserCredentials> UsersCredentials { get; }
        Task<IClientSessionHandle> StartSessionAsync();
    }
}
