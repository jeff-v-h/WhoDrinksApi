using WhoDrinks.Api.Helpers;
using WhoDrinks.Api.Helpers.Extensions;
using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.Exceptions;
using WhoDrinks.Api.Models.RequestModels;
using WhoDrinks.Api.Models.ResponseModels;
using WhoDrinks.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IPasswordHelper _passwordHelper;
        private readonly ILogger<UsersManager> _logger;

        public UsersManager(IUsersRepository userRepo, IPasswordHelper passwordHelper, ILogger<UsersManager> logger)
        {
            _usersRepo = userRepo;
            _passwordHelper = passwordHelper;
            _logger = logger;
        }

        public async Task<User> GetUser(string id)
        {
            return await _usersRepo.GetUser(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _usersRepo.GetUserByEmail(email);
        }

        // To create user with email, use SignUp method instead
        public async Task<string> CreateUser(CreateUserRequest request)
        {
            var user = new User
            {
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
            await _usersRepo.CreateUser(user);
            return user.Id;
        }

        public async Task UpdateUser(string id, UpdateUserRequest request)
        {
            var user = await _usersRepo.GetUser(id);
            if (user == null) {
                throw new KeyNotFoundException();
            }
            if (!request.Email.IsNullOrEmpty() && user.Email != request.Email && await _usersRepo.UserEmailExists(request.Email)) {
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

            await _usersRepo.UpdateUser(id, user);
        }

        public async Task<string> SignUpUser(SignUpRequest request)
        {
            if (await _usersRepo.UserEmailExists(request.Email)) {
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

            // Use Transact if using replica sets for db
            await SaveUserAndCredentials(request.Id, user, credentials);
            //await _usersRepo.Transact(async session => await SaveUserAndCredentials(request.Id, user, credentials, session));

            return user.Id;
        }

        private async Task SaveUserAndCredentials(string id, User user, UserCredentials credentials, IClientSessionHandle session = null)
        {
            if (id.IsNullOrEmpty()) {
                await _usersRepo.CreateUser(user, session);
            } else {
                await _usersRepo.UpdateUser(id, user, session);
            }

            await _usersRepo.CreateCredentials(credentials, session);
        }

        public async Task<LoginResponse> Authenticate(string email, string password)
        {
            var userCredentials = await _usersRepo.GetUserCredentials(email);

            if (userCredentials == null) {
                var msg = $"User with email {email} could not be found.";
                _logger.LogError(msg);
                throw new KeyNotFoundException(msg);
            }

            (var verified, var needsUpgrade) = _passwordHelper.Check(userCredentials.Hashed, password);

            if (!verified) {
                return null;
            }

            var user = await _usersRepo.GetUser(userCredentials.UserId);

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
