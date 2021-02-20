using DontThinkJustDrink.Api.Entities;
using MongoDB.Driver;

namespace DontThinkJustDrink.Api.Data.Interfaces
{
    public interface IMainAppContext
    {
        IMongoCollection<AppVersion> AppVersions { get; }
    }
}
