using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.Trip
{
    public class TripUpdateDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool Active { get; set; }
    }
}
