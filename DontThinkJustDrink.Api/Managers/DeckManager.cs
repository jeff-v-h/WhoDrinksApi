using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
{
    public class DeckManager : IDeckManager
    {
        private readonly IDeckRepository _deckRepo;

        public DeckManager(IDeckRepository deckRepo)
        {
            _deckRepo = deckRepo;
        }

        public Task<Deck> Get(string id) => _deckRepo.Get(id);
    }
}
