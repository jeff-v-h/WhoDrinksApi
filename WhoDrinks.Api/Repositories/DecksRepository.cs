using WhoDrinks.Api.Data.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Repositories
{
    public class DecksRepository : IDecksRepository
    {
        private readonly IMainAppContext _context;

        public DecksRepository(IMainAppContext context)
        {
            _context = context;
        }

        public async Task<List<DeckData>> GetList()
        {
            var projection = Builders<Deck>.Projection
                .Include("Id")
                .Include("UserId")
                .Include("Name")
                .Include("Tags");
            return await _context.Decks.Find(_ => true).Project<DeckData>(projection).ToListAsync();
        }

        public async Task<Deck> Get(string id) =>
            await _context.Decks.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task Create(Deck deck) =>
            await _context.Decks.InsertOneAsync(deck);
    }
}
