using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        public Task CreateUser(User user, UserCredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
