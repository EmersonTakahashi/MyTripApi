using Microsoft.AspNetCore.Identity;

namespace MyTripApi.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
