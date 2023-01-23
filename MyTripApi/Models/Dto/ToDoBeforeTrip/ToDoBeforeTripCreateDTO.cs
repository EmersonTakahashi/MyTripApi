using System.ComponentModel.DataAnnotations;

namespace MyTripApi.Models.Dto.Trip
{
    public class ToDoBeforeTripCreateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ToDoUntil { get; set; }
    }
}
