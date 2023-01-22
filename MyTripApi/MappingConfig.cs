using AutoMapper;
using MyTripApi.Models;
using MyTripApi.Models.Dto.Trip;

namespace MyTripApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Trip, TripDTO>().ReverseMap();
            CreateMap<Trip, TripCreateDTO>().ReverseMap();
            CreateMap<Trip, TripUpdateDTO>().ReverseMap();
        }
    }
}
