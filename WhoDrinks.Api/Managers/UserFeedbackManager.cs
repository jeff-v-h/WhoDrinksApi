using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.RequestModels;
using WhoDrinks.Api.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers
{
    public class UserFeedbackManager : IUserFeedbackManager
    {
        private readonly IUserFeedbackRepository _feedbackRepo;

        public UserFeedbackManager(IUserFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        public async Task Create(UserFeedbackRequest request)
        {
            await _feedbackRepo.Create(new UserFeedback
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Feedback = request.Feedback,
                UserCreatedOn = request.UserCreatedOn,
                CreatedOn = DateTime.UtcNow
            });
        }
    }
}
