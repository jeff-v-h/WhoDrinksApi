using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.Exceptions;
using WhoDrinks.Api.Models.RequestModels;
using WhoDrinks.Api.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager _usersManager;

        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> Get([FromQuery(Name = "Id")] string id, [FromQuery(Name = "Email")] string email, [FromQuery(Name = "DeviceId")] string deviceId)
        {
            if (id == null && email == null) {
                return BadRequest(new ErrorDetails(400, "Either Id or Email query param is required"));
            }

            var user = id != null
                ? await _usersManager.GetUser(id)
                : await _usersManager.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IdResponse>> Create([FromBody] CreateUserRequest request)
        {
            return Ok(new IdResponse
            {
                Id = await _usersManager.CreateUser(request)
            });
        }

        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateUserRequest request)
        {
            try {
                await _usersManager.UpdateUser(id, request);
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
                    Id = await _usersManager.SignUpUser(request)
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
                var response = await _usersManager.Authenticate(request.Email, request.Password);
                return response == null ? BadRequest() : Ok(response);
            } catch (KeyNotFoundException) {
                return BadRequest();
            }
        }
    }
}
