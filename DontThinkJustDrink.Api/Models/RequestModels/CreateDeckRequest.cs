using DontThinkJustDrink.Api.Helpers.Enums;
using System.Collections.Generic;

namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class CreateDeckRequest
    {
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public List<GameTypes> Tags { get; set; }
    }
}
