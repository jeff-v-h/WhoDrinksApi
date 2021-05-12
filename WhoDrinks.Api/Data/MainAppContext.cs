using WhoDrinks.Api.Data.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Settings.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Data
{
    public class MainAppContext : IMainAppContext
    {
        public IMongoCollection<AppVersion> AppVersions { get; }
        public IMongoCollection<UserFeedback> UsersFeedback { get; }
        public IMongoCollection<User> Users { get; }
        public IMongoCollection<UserCredentials> UsersCredentials { get; }
        public IMongoCollection<Deck> Decks { get; }
        private MongoClient _mongoClient { get; }

        public MainAppContext(IMainAppDatabaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            var database = _mongoClient.GetDatabase(settings.DatabaseName);

            AppVersions = database.GetCollection<AppVersion>(settings.AppVersionCollectionName);
            UsersFeedback = database.GetCollection<UserFeedback>(settings.UsersFeedbackCollectionName);
            Users = database.GetCollection<User>(settings.UsersCollectionName);
            UsersCredentials = database.GetCollection<UserCredentials>(settings.UserCredentialsCollectionName);
            Decks = database.GetCollection<Deck>(settings.DecksCollectionName);
        }

        public async Task<IClientSessionHandle> StartSessionAsync() => await _mongoClient.StartSessionAsync();
    }
}
