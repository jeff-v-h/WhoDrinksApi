using System.Collections.Generic;

namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class SignUpRequest : User
    {
        public string Password { get; set; }
    }
}
