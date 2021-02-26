using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Settings;
using MongoDB.Driver;

namespace DontThinkJustDrink.Api.Data
{
    public class MainAppContext : IMainAppContext
    {
        public MainAppContext(IMainAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            AppVersions = database.GetCollection<AppVersion>(settings.AppVersionCollectionName);
            UsersFeedback = database.GetCollection<UserFeedback>(settings.UserFeedbackCollectionName);
            Users = database.GetCollection<User>(settings.UsersCollectionName);
            UsersCredentials = database.GetCollection<UserCredentials>(settings.UserCredentialsCollectionName);
        }

        public MongoClient MongoClient { get; }
        public IMongoCollection<AppVersion> AppVersions { get; }
        public IMongoCollection<UserFeedback> UsersFeedback { get; }
        public IMongoCollection<User> Users { get; }
        public IMongoCollection<UserCredentials> UsersCredentials { get; }
    }
}
