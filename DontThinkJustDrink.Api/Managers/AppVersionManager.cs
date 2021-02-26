using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
{
    public class AppVersionManager : IAppVersionManager
    {
        private readonly IAppVersionRepository _appVersionRepository;

        public AppVersionManager(IAppVersionRepository appVersionRepository)
        {
            _appVersionRepository = appVersionRepository;
        }

        public Task<AppVersion> GetDetails(string version)
        {
            return _appVersionRepository.GetDetails(version);
        }
    }
}
