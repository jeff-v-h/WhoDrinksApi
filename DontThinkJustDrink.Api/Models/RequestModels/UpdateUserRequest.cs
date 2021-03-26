using DontThinkJustDrink.Api.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class UpdateUserRequest : User
    {
        [Required]
        public new string Email { get; set; }
    }
}
