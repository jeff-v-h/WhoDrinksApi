using WhoDrinks.Api.Settings.Interfaces;

namespace WhoDrinks.Api.Settings
{
    public class MainAppDatabaseSettings : IMainAppDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AppVersionCollectionName { get; set; }
        public string UsersFeedbackCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string UserCredentialsCollectionName { get; set; }
        public string DecksCollectionName { get; set; }
    }
}
