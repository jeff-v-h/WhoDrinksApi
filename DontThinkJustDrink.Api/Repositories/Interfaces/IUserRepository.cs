using DontThinkJustDrink.Api.Models;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user, UserCredentials credentials);
    }
}
