namespace DontThinkJustDrink.Api.Settings
{
    public interface IMainAppDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AppVersionCollectionName { get; set; }
        string UserFeedbackCollectionName { get; set; }
    }
}
