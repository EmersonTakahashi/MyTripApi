using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.Trip
{
    public class TripCreateDTO
    {        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }        
    }
}
