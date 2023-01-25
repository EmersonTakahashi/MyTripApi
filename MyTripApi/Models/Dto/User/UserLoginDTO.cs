using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.User
{
    public class UserLoginDTO : UserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Your password needs to be great than 8 characters")]
        public string Password { get; set; }
    }
}
