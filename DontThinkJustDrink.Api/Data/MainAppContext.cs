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
        }

        public IMongoCollection<AppVersion> AppVersions { get; }
    }
}
