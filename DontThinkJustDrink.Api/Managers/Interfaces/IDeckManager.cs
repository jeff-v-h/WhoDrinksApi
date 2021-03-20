using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IDeckManager
    {
        Task<Deck> Get(string id);
        Task<string> Create(CreateDeckRequest deck);
    }
}
