using DontThinkJustDrink.Api.Models;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task<bool> CreateUser(User user, UserCredentials credentials);
    }
}
