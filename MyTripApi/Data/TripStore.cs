using MyTripApi.Models.Dto;

namespace MyTripApi.Data
{
    public static class TripStore
    {
        public static List<TripDTO> tripList = new List<TripDTO>{
            new TripDTO{Id = Guid.NewGuid(), Name = "Brazil"},
            new TripDTO{Id = Guid.NewGuid(), Name = "Disney"}
};
    }
}
