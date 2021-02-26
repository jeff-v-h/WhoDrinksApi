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
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHelper _passwordHelper;

        public UserManager(IUserRepository authRepo, IPasswordHelper passwordHelper)
        {
            _userRepo = authRepo;
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

            var credentials = new UserCredentials
            {
                Email = request.Email,
                Hashed = _passwordHelper.Hash(request.Password)
            };

            return _userRepo.CreateUser(user, credentials);
        }
    }
}
