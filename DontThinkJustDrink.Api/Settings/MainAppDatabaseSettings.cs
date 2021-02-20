namespace DontThinkJustDrink.Api.Settings
{
    public class MainAppDatabaseSettings : IMainAppDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
