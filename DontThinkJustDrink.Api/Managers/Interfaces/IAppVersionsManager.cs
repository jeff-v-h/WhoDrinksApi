using DontThinkJustDrink.Api.Models.Database;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IAppVersionsManager
    {
        Task<AppVersion> GetDetails(string version);
    }
}
