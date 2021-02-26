using DontThinkJustDrink.Api.Helpers;
using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAuthRepository _authRepo;
        private readonly IPasswordHelper _passwordHelper;

        public UserManager(IUserAuthRepository authRepo, IPasswordHelper passwordHelper)
        {
            _authRepo = authRepo;
            _passwordHelper = passwordHelper;
        }

        public Task<bool> SignUpUser(SignUpRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            (var salt, var hashed) = _passwordHelper.GetPasswordSaltAndHash(request.Password);

            var credentials = new UserCredentials
            {
                Email = request.Email,
                Salt = salt,
                Hashed = hashed
            };

            return _authRepo.CreateUser(user, credentials);
        }
    }
}
