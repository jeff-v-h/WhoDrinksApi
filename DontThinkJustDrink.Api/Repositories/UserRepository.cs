using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMainAppContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IMainAppContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateUser(User user, UserCredentials credentials)
        {
            using var session = await _context.MongoClient.StartSessionAsync();
            session.StartTransaction();

            try {
                await _context.Users.InsertOneAsync(session, user);
                credentials.UserId = user.Id;
                await _context.UsersCredentials.InsertOneAsync(session, credentials);

                await session.CommitTransactionAsync();
                return true;
            } catch (Exception e) {
                _logger.LogError(e, $"Unable to complete creation of user with auth for email: {user.Email}; full name: {user.FirstName} {user.LastName}");
                await session.AbortTransactionAsync();
                return false;
            }
        }
    }
}
