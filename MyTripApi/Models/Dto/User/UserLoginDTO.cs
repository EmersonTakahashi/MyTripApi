using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.User
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
