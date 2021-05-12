using WhoDrinks.Api.Data.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories
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
            await _context.UsersFeedback.InsertOneAsync(feedback);
        }
    }
}
