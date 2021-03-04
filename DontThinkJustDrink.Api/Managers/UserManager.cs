using DontThinkJustDrink.Api.Helpers;
using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHelper _passwordHelper;
        private readonly ILogger<UserManager> _logger;

        public UserManager(IUserRepository authRepo, IPasswordHelper passwordHelper, ILogger<UserManager> logger)
        {
            _userRepo = authRepo;
            _passwordHelper = passwordHelper;
            _logger = logger;
        }

        public async Task SignUpUser(SignUpRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var credentials = new UserCredentials
            {
                Email = request.Email,
                Hashed = _passwordHelper.Hash(request.Password)
            };

            await _userRepo.CreateUser(user, credentials);
        }

        public async Task<LoginResponse> Authenticate(string email, string password)
        {
            var userCredentials = await _userRepo.GetUserCredentials(email);

            if (userCredentials == null) {
                var msg = $"User with email {email} could not be found.";
                _logger.LogError(msg);
                throw new KeyNotFoundException(msg);
            }

            (var verified, var needsUpgrade) = _passwordHelper.Check(userCredentials.Hashed, password);
            var response = new LoginResponse
            {
                IsVerified = verified
            };

            if (!verified) {
                return response;
            }

            var user = await _userRepo.GetUser(userCredentials.UserId);

            if (user == null) {
                _logger.LogError($"User with id {userCredentials.UserId} could not be found");
                throw new KeyNotFoundException();
            }

            response.ShouldUpdatePassword = needsUpgrade;
            response.Username = user.Username;
            return response;
        }
    }
}
