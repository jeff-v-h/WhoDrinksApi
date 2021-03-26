using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
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
