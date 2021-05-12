using WhoDrinks.Api.Managers.Interfaces;
using WhoDrinks.Api.Models.Database;
using WhoDrinks.Api.Models.RequestModels;
using WhoDrinks.Api.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WhoDrinks.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly IDecksManager _decksManager;

        public DecksController(IDecksManager decksManager)
        {
            _decksManager = decksManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DeckData>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<DeckData>>> GetDeckList()
        {
            var decks = await _decksManager.GetList();
            return Ok(decks);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Deck), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Deck>> Get(string id)
        {
            return Ok(await _decksManager.Get(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IdResponse>> Create([FromBody] CreateDeckRequest request)
        {
            return Ok(new IdResponse
            {
                Id = await _decksManager.Create(request)
            });
        }
    }
}
