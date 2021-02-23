using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppVersionController : ControllerBase
    {
        private readonly IAppVersionRepository _appVersionRepo;
        private readonly ILogger<AppVersionController> _logger;

        public AppVersionController(IAppVersionRepository appVersionRepo, ILogger<AppVersionController> logger)
        {
            _appVersionRepo = appVersionRepo;
            _logger = logger;
        }

        [HttpGet("{version}")]
        public async Task<AppVersion> Get(string version)
        {
            return await _appVersionRepo.GetDetails(version);
        }
    }
}
