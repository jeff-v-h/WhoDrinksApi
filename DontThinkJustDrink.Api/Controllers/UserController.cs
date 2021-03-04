using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.Exceptions;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try {
                await _userManager.SignUpUser(request);
                return Ok();
            } catch (DuplicateEmailException) {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = $"User already exists for email: {request.Email}"
                });
            }
        }

        [HttpPost("auth")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
        {
            return Ok(await _userManager.Authenticate(request.Email, request.Password));
        }
    }
}
