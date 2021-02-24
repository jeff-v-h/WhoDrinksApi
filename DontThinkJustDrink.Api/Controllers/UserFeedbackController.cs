using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFeedbackController : ControllerBase
    {
        private readonly IUserFeedbackRepository _feedbackRepo;

        public UserFeedbackController(IUserFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserFeedback), (int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<UserFeedback>> Create([FromBody] UserFeedback feedback)
        {
            await _feedbackRepo.Create(feedback);

            return Accepted();
        }
    }
}
