namespace DontThinkJustDrink.Api.Settings
{
    public interface IMainAppDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
