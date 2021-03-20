using DontThinkJustDrink.Api.Models.Database;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IDeckRepository
    {
        Task<Deck> Get(string id);
    }
}
