using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyTripApi.Models.Dto.Trip
{
    public class ToDoBeforeTripCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public Guid TripId { get; set; }               
        public DateTime? ToDoUntil { get; set; }
    }
}
