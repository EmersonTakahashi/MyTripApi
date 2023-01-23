using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.Trip
{
    public class ToDoBeforeTripUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool Active { get; set; }    
        public DateTime? ToDoUntil { get; set; }
        [Required]
        public Guid TripId { get; set; }
    }
}
