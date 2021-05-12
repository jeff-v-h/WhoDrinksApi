using WhoDrinks.Api.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace WhoDrinks.Api.Models.RequestModels
{
    public class UpdateUserRequest : User
    {
        [Required]
        public new string Email { get; set; }
    }
}
