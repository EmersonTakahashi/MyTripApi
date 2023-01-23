using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.Trip
{
    public class ToDoBeforeTripDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime? ToDoUntil { get; set; }
        [Required]
        public Guid TripId { get; set; }
    }
}
