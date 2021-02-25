using DontThinkJustDrink.Api.Models;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task CreateUser(User user, UserCredentials credentials);
    }
}
