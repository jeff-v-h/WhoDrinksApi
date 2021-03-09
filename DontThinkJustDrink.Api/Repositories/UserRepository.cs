using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.Exceptions;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Linq;
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

        public async Task CreateUser(User user, UserCredentials credentials)
        {
            using var session = await _context.StartSessionAsync();

            try {
                session.StartTransaction();

                await _context.Users.InsertOneAsync(session, user);
                credentials.UserId = user.Id;
                await _context.UsersCredentials.InsertOneAsync(session, credentials);

                await session.CommitTransactionAsync();
            } catch (MongoWriteException mwe) when (IsDuplicateEmailError(mwe)) {
                _logger.LogError(mwe, $"Unable to complete creation of user with auth for email: {user.Email}; full name: {user.FirstName} {user.LastName}");
                await session.AbortTransactionAsync();
                throw new DuplicateEmailException();
            }
        }

        private bool IsDuplicateEmailError(MongoWriteException ex)
        {
            var writeError = (ex.InnerException as MongoBulkWriteException)?.WriteErrors
                .FirstOrDefault(we => we.Category == ServerErrorCategory.DuplicateKey && we.Code == 11000);
            
            if (writeError == null) {
                return false;
            }

            return writeError.Message.Contains("Email:");
        }

        public async Task<User> GetUser(string id)
        {
            return await _context.Users.Find(uc => uc.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserCredentials> GetUserCredentials(string email)
        {
            return await _context.UsersCredentials.Find(uc => uc.Email == email).FirstOrDefaultAsync();
        }
    }
}
