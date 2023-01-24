using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTripApi.Models.Entities
{
    public class ToDoBeforeTrip
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? ToDoUntil { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //ForeignKey
        [ForeignKey("Trip")]
        public Guid TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}
