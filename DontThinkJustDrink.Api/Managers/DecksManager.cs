using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers
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
