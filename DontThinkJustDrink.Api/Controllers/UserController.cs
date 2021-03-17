using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.Exceptions;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace DontThinkJustDrink.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{deviceId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUser([FromQuery(Name="Email")] string email, [FromQuery(Name = "DeviceId")] string deviceId)
        {
            var user = email != null
                ? await _userManager.GetUserByEmail(email)
                : await _userManager.GetUserByDeviceId(deviceId);
            return Ok(user);
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
        {
            try {
                var response = await _userManager.Authenticate(request.Email, request.Password);
                return response == null ? BadRequest() : Ok(response);
            } catch (KeyNotFoundException) {
                return BadRequest();
            }
        }
    }
}
