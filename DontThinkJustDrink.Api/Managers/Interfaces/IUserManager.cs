using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IUserManager
    {
        Task SignUpUser(SignUpRequest request);
        Task<LoginResponse> Authenticate(string email, string password);
    }
}
