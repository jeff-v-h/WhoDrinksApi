using DontThinkJustDrink.Api.Models;
using MongoDB.Driver;

namespace DontThinkJustDrink.Api.Data.Interfaces
{
    public interface IMainAppContext
    {
        IMongoCollection<AppVersion> AppVersions { get; }
    }
}
