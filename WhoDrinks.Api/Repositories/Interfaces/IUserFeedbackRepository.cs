using WhoDrinks.Api.Models.Database;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories.Interfaces
{
    public interface IUserFeedbackRepository
    {
        Task Create(UserFeedback feedback);
    }
}
