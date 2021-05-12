using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.RequestModels;
using WhoDrinks.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Managers
{
    public class DecksManager : IDecksManager
    {
        private readonly IDecksRepository _decksRepo;

        public DecksManager(IDecksRepository decksRepo)
        {
            _decksRepo = decksRepo;
        }

        public async Task<List<DeckData>> GetList() => await _decksRepo.GetList();

        public async Task<Deck> Get(string id) => await _decksRepo.Get(id);

        public async Task<string> Create(CreateDeckRequest request)
        {
            var deck = new Deck
            {
                Name = request.Name,
                Cards = request.Cards,
                Tags = request.Tags,
                UserId = request.UserId
            };
            await _decksRepo.Create(deck);
            return deck.Id;
        }
    }
}
