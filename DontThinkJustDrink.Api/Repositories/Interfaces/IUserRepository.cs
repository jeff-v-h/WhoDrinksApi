using DontThinkJustDrink.Api.Models;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user, UserCredentials credentials);
        Task<User> GetUser(string id);
        Task<UserCredentials> GetUserCredentials(string email);
    }
}
