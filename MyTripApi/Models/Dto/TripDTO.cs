using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto
{
    public class TripDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
