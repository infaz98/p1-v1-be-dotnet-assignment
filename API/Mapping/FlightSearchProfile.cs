using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Microsoft.AspNetCore.Mvc;

namespace API.Mapping
{
    public class FlightSearchProfile : Profile
    {
        public FlightSearchProfile()
        {
            CreateMap<Flight, FlightViewModel>();
        }
    }
}