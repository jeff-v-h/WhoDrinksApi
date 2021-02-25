namespace DontThinkJustDrink.Api.Settings
{
    public class MainAppDatabaseSettings : IMainAppDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AppVersionCollectionName { get; set; }
        public string UserFeedbackCollectionName { get; set; }
    }
}
