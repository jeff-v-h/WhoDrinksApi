using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers.Interfaces
{
    public interface IDecksManager
    {
        Task<List<DeckData>> GetList();
        Task<Deck> Get(string id);
        Task<string> Create(CreateDeckRequest deck);
    }
}
