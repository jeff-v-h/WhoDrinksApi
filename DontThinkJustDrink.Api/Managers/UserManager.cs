using DontThinkJustDrink.Api.Extensions;
using DontThinkJustDrink.Api.Helpers;
using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.Exceptions;
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

        public async Task<string> CreateUser(CreateUserRequest request)
        {
            if (!request.Email.IsNullOrEmpty() && await _userRepo.UserEmailExists(request.Email)) {
                throw new DuplicateEmailException();
            }

            var user = new User
            {
                Email = request.Email,
                DeviceIds = new List<string>()
                {
                    request.DeviceId
                },
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ConfirmedDisclaimer = request.ConfirmedDisclaimer,
                CurrentAppVersion = request.CurrentAppVersion
            };
            await _userRepo.CreateUser(user);
            return user.Id;
        }

        public async Task UpdateUser(string id, UpdateUserRequest request)
        {
            var user = await _userRepo.GetUser(id);
            if (!request.Email.IsNullOrEmpty() && user.Email != request.Email && await _userRepo.UserEmailExists(request.Email)) {
                throw new DuplicateEmailException();
            }

            user.Id = request.Id;
            user.Email = request.Email;
            user.DeviceIds = request.DeviceIds;
            user.Username = request.Username;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.ConfirmedDisclaimer = request.ConfirmedDisclaimer;
            user.AgreedToPrivacyPolicy = request.AgreedToPrivacyPolicy;
            user.AgreedToTCs = request.AgreedToTCs;
            user.PrivacyPolicyVersionAgreedTo = request.PrivacyPolicyVersionAgreedTo;
            user.TCsVersionAgreedTo = request.TCsVersionAgreedTo;
            user.CurrentAppVersion = request.CurrentAppVersion;

            await _userRepo.UpdateUser(id, user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepo.GetUserByEmail(email);
        }

        public async Task<User> GetUserByDeviceId(string deviceId)
        {
            return await _userRepo.GetUserByDeviceId(deviceId);
        }

        public async Task SignUpUser(SignUpRequest request)
        {
            if (await _userRepo.UserEmailExists(request.Email)) {
                throw new DuplicateEmailException();
            }

            var user = new User
            {
                Id = request.Id,
                Email = request.Email,
                DeviceIds = request.DeviceIds,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ConfirmedDisclaimer = request.ConfirmedDisclaimer,
                AgreedToPrivacyPolicy = request.AgreedToPrivacyPolicy,
                AgreedToTCs = request.AgreedToTCs,
                PrivacyPolicyVersionAgreedTo = request.PrivacyPolicyVersionAgreedTo,
                TCsVersionAgreedTo = request.TCsVersionAgreedTo,
                CurrentAppVersion = request.CurrentAppVersion
            };

            var credentials = new UserCredentials
            {
                Email = request.Email,
                Hashed = _passwordHelper.Hash(request.Password)
            };

            await _userRepo.Transact(async session =>
            {
                if (request.Id.IsNullOrEmpty()) {
                    await _userRepo.CreateUser(user, session);
                } else {
                    await _userRepo.UpdateUser(request.Id, user, session);
                }

                await _userRepo.CreateCredentials(credentials, session);
            });
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

            if (!verified) {
                return null;
            }

            var user = await _userRepo.GetUser(userCredentials.UserId);

            if (user == null) {
                _logger.LogError($"User with id {userCredentials.UserId} could not be found");
                throw new KeyNotFoundException();
            }

            return new LoginResponse
            {
                Username = user.Username,
                ShouldUpdatePassword = needsUpgrade
            };
        }
    }
}
