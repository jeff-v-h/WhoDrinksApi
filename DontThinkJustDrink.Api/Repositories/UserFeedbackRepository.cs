using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories
{
    public class UserFeedbackRepository : IUserFeedbackRepository
    {
        private readonly IMainAppContext _context;

        public UserFeedbackRepository(IMainAppContext context)
        {
            _context = context;
        }

        public async Task Create(UserFeedback feedback)
        {
            await _context.UserFeedback.InsertOneAsync(feedback);
        }
    }
}
