using WhoDrinks.Api.Models.Database;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories.Interfaces
{
    public interface IAppVersionsRepository
    {
        Task<AppVersion> GetDetails(string version);
    }
}
