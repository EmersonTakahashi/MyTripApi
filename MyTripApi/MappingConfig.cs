﻿using AutoMapper;
using MyTripApi.Models.Dto.Trip;
using MyTripApi.Models.Entities;

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
