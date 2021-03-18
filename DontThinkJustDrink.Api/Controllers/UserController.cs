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

        [HttpGet]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> Get([FromQuery(Name = "Id")] string id, [FromQuery(Name = "Email")] string email, [FromQuery(Name = "DeviceId")] string deviceId)
        {
            if (id == null && email == null) {
                return BadRequest(new ErrorDetails(400, "Either Id or Email query param is required"));
            }

            var user = id != null
                ? await _userManager.GetUser(id)
                : await _userManager.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            return Ok(new CreateUserResponse
            {
                Id = await _userManager.CreateUser(request)
            });
        }

        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateUserRequest request)
        {
            try {
                await _userManager.UpdateUser(id, request);
                return Ok();
            } catch (DuplicateEmailException) {
                return BadRequest(new ErrorDetails(400, $"User already exists for email: {request.Email}"));
            } catch (KeyNotFoundException) {
                return BadRequest(new ErrorDetails(400, $"No User found with id: {id}"));
            }
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(SignUpResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<SignUpResponse>> SignUp([FromBody] SignUpRequest request)
        {
            try {
                return Ok(new SignUpResponse
                {
                    Id = await _userManager.SignUpUser(request)
                });
            } catch (DuplicateEmailException) {
                return BadRequest(new ErrorDetails(400, $"User already exists for email: {request.Email}"));
            }
        }

        [HttpPost("auth")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
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
