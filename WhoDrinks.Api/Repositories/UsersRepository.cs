using WhoDrinks.Api.Data.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMainAppContext _context;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(IMainAppContext context, ILogger<UsersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Transact(Action<IClientSessionHandle> action)
        {
            using var session = await _context.StartSessionAsync();
            
            try {
                session.StartTransaction();
                action(session);
                await session.CommitTransactionAsync();
            } catch (Exception e) {
                var msg = e.Message;
                await session.AbortTransactionAsync();
                throw;
            }
        }

        public async Task<User> GetUser(string id) =>
            await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<User> GetUserByEmail(string email) =>
            await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<UserCredentials> GetUserCredentials(string email) =>
            await _context.UsersCredentials.Find(uc => uc.Email == email).FirstOrDefaultAsync();

        public async Task<bool> UserEmailExists(string email) =>
            await _context.Users.CountDocumentsAsync(u => u.Email == email) > 0;

        public async Task CreateUser(User user, IClientSessionHandle session = null)
        {
            if (session != null) {
                await _context.Users.InsertOneAsync(session, user);
                return;
            }
            await _context.Users.InsertOneAsync(user);
        }

        public async Task UpdateUser(string id, User user, IClientSessionHandle session = null)
        {
            if (session != null) {
                await _context.Users.ReplaceOneAsync(session, u => u.Id == id, user);
                return;
            }
            await _context.Users.ReplaceOneAsync(u => u.Id == id, user);
        }

        public async Task CreateCredentials(UserCredentials credentials, IClientSessionHandle session = null)
        {
            if (session != null) {
                await _context.UsersCredentials.InsertOneAsync(session, credentials);
                return;
            }
            await _context.UsersCredentials.InsertOneAsync(credentials);
        }
    }
}
