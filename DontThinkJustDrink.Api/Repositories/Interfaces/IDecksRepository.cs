using DontThinkJustDrink.Api.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories.Interfaces
{
    public interface IDecksRepository
    {
        Task<List<DeckData>> GetList();
        Task<Deck> Get(string id);
        Task Create(Deck deck);
    }
}
