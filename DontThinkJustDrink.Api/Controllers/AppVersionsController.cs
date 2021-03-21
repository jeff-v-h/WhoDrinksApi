using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
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
    public class AppVersionsController : ControllerBase
    {
        private readonly IAppVersionManager _appVersionManager;
        private readonly ILogger<AppVersionsController> _logger;

        public AppVersionsController(IAppVersionManager appVersionManager, ILogger<AppVersionsController> logger)
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
