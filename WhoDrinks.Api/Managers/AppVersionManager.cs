using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers
{
    public class AppVersionManager : IAppVersionsManager
    {
        private readonly IAppVersionsRepository _appVersionsRepository;

        public AppVersionManager(IAppVersionsRepository appVersionsRepository)
        {
            _appVersionsRepository = appVersionsRepository;
        }

        public Task<AppVersion> GetDetails(string version)
        {
            return _appVersionsRepository.GetDetails(version);
        }
    }
}
