using DontThinkJustDrink.Api.Models.Database;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IAppVersionRepository
    {
        Task<AppVersion> GetDetails(string version);
    }
}
