using DontThinkJustDrink.Api.Models.Database;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IAppVersionsRepository
    {
        Task<AppVersion> GetDetails(string version);
    }
}
