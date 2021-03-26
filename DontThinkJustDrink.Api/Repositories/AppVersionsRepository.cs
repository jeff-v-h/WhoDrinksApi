using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories
{
    public class AppVersionsRepository : IAppVersionsRepository
    {
        private readonly IMainAppContext _context;

        public AppVersionsRepository(IMainAppContext context)
        {
            _context = context;
        }

        public async Task<AppVersion> GetDetails(string version)
        {
            return await _context.AppVersions.Find(v => v.Version == version).FirstOrDefaultAsync();
        }
    }
}
