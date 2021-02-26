using DontThinkJustDrink.Api.Models;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IAppVersionManager
    {
        Task<AppVersion> GetDetails(string version);
    }
}
