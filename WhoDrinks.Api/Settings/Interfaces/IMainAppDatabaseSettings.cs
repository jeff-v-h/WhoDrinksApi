namespace WhoDrinks.Api.Settings.Interfaces
{
    public interface IMainAppDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AppVersionCollectionName { get; set; }
        string UsersFeedbackCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string UserCredentialsCollectionName { get; set; }
        string DecksCollectionName { get; set; }
    }
}
