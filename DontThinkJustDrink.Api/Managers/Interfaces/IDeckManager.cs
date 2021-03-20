using DontThinkJustDrink.Api.Models.Database;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IDeckManager
    {
        Task<Deck> Get(string id);
    }
}
