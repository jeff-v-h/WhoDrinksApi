using WhoDrinks.Api.Models.RequestModels;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers.Interfaces
{
    public interface IUserFeedbackManager
    {
        Task Create(UserFeedbackRequest request);
    }
}
