using WhoDrinks.Api.Helpers.Enums;
using System.Collections.Generic;

namespace WhoDrinks.Api.Models.RequestModels
{
    public class CreateDeckRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public List<GameTypes> Tags { get; set; }
    }
}
