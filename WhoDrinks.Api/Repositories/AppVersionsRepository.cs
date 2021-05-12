using WhoDrinks.Api.Data.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Repositories.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories
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
