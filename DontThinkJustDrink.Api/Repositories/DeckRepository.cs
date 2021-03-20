﻿using DontThinkJustDrink.Api.Data.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Repositories.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly IMainAppContext _context;

        public DeckRepository(IMainAppContext context)
        {
            _context = context;
        }

        public async Task<Deck> Get(string id) =>
            await _context.Decks.Find(d => d.Id == id).FirstOrDefaultAsync();

        public async Task Create(Deck deck) =>
            await _context.Decks.InsertOneAsync(deck);
    }
}
