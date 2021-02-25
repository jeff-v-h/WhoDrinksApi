﻿using DontThinkJustDrink.Api.Managers.Interfaces;
using DontThinkJustDrink.Api.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFeedbackController : ControllerBase
    {
        private readonly IUserFeedbackManager _feedbackManager;

        public UserFeedbackController(IUserFeedbackManager feedbackManager)
        {
            _feedbackManager = feedbackManager;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> Create([FromBody] UserFeedbackRequest request)
        {
            await _feedbackManager.Create(request);
            return Accepted();
        }
    }
}