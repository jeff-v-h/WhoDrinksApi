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
        private readonly IAppVersionsManager _appVersionsManager;
        private readonly ILogger<AppVersionsController> _logger;

        public AppVersionsController(IAppVersionsManager appVersionsManager, ILogger<AppVersionsController> logger)
        {
            _appVersionsManager = appVersionsManager;
            _logger = logger;
        }

        [HttpGet("test/{text}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppVersion), (int)HttpStatusCode.OK)]
        public ActionResult<TestResponse> TestEndpoint(string text)
        {
            return Ok(new TestResponse
            {
                Text = text
            });
        }

        [HttpGet("{version}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppVersion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AppVersion>> GetAppVersion(string version)
        {
            var versionDetails = await _appVersionsManager.GetDetails(version);

            if (versionDetails == null)
            {
                _logger.LogError($"Details for app version {version} could not be found in database.");
                return NotFound();
            }

            return Ok(versionDetails);
        }
    }

    public class TestResponse
    {
        public string Text { get; set; }
    }
}
