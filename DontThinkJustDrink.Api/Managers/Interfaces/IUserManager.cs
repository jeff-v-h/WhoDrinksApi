using DontThinkJustDrink.Api.Models.RequestModels;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<bool> SignUpUser(SignUpRequest request);
    }
}
