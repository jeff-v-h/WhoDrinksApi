using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
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

        public async Task<Deck> Get(string id) => await _deckRepo.Get(id);

        public async Task<string> Create(CreateDeckRequest request)
        {
            var deck = new Deck
            {
                Name = request.Name,
                Cards = request.Cards,
                Tags = request.Tags
            };
            await _deckRepo.Create(deck);
            return deck.Id;
        }
    }
}
