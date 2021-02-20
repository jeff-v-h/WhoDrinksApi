using DontThinkJustDrink.Api.Entities;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IAppVersionRepository
    {
        Task<AppVersion> GetDetails(string version);
    }
}
