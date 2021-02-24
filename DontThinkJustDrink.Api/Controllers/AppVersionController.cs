using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
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
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppVersion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AppVersion>> GetAppVersion(string version)
        {
            var versionDetails = await _appVersionRepo.GetDetails(version);

            if (versionDetails == null)
            {
                _logger.LogError($"Details for app version {version} could not be found in database.");
                return NotFound();
            }

            return Ok(versionDetails);
        }
    }
}
