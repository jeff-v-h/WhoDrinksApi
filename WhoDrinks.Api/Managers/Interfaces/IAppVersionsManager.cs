using WhoDrinks.Api.Models.Database;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers.Interfaces
{
    public interface IAppVersionsManager
    {
        Task<AppVersion> GetDetails(string version);
    }
}
