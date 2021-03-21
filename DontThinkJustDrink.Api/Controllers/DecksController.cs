using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly IDeckManager _deckManager;

        public DecksController(IDeckManager deckManager)
        {
            _deckManager = deckManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DeckData>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Deck>> GetList()
        {
            return Ok(await _deckManager.GetList());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Deck), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Deck>> Get(string id)
        {
            return Ok(await _deckManager.Get(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IdResponse>> Create([FromBody] CreateDeckRequest request)
        {
            return Ok(new IdResponse
            {
                Id = await _deckManager.Create(request)
            });
        }
    }
}
