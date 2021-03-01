using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Exceptions;
using DontThinkJustDrink.Api.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;


namespace DontThinkJustDrink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try {
                await _userManager.SignUpUser(request);
                return Accepted();
            } catch (DuplicateEmailException) {
                return BadRequest($"User already exists for email: {request.Email}");
            }
        }
    }
}
