using DontThinkJustDrink.Api.Models.RequestModels;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IUserFeedbackManager
    {
        Task Create(UserFeedbackRequest request);
    }
}
