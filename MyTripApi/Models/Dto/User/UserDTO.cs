using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.User
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserName { get; set; }        

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Your password must have 8 characters")]
        public string Password { get; set; }
    }
}
