using DontThinkJustDrink.Api.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class SignUpRequest : User
    {
        [Required]
        public new string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
