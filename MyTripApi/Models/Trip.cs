using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models
{
    public class Trip
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
    }
}
