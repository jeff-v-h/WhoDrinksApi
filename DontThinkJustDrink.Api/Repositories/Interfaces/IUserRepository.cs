using DontThinkJustDrink.Api.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Transact(Action<IClientSessionHandle> action);
        Task<User> GetUser(string id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByDeviceId(string deviceId);
        Task<UserCredentials> GetUserCredentials(string email);
        Task<bool> UserEmailExists(string email);
        Task CreateUser(User user, IClientSessionHandle session = null);
        Task UpdateUser(string id, User user, IClientSessionHandle session = null);
        Task CreateCredentials(UserCredentials credentials, IClientSessionHandle session = null);
    }
}
