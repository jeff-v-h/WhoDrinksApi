using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppVersionController : ControllerBase
    {
        private readonly IAppVersionManager _appVersionManager;
        private readonly ILogger<AppVersionController> _logger;

        public AppVersionController(IAppVersionManager appVersionManager, ILogger<AppVersionController> logger)
        {
            _appVersionManager = appVersionManager;
            _logger = logger;
        }

        [HttpGet("{version}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppVersion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AppVersion>> GetAppVersion(string version)
        {
            var versionDetails = await _appVersionManager.GetDetails(version);

            if (versionDetails == null)
            {
                _logger.LogError($"Details for app version {version} could not be found in database.");
                return NotFound();
            }

            return Ok(versionDetails);
        }
    }
}
