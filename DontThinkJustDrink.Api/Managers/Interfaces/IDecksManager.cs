using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IDecksManager
    {
        Task<List<DeckData>> GetList();
        Task<Deck> Get(string id);
        Task<string> Create(CreateDeckRequest deck);
    }
}
