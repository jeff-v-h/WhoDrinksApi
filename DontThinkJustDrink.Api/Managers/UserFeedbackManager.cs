using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
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
