using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;

namespace API.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Airport, AirportViewModel>();

        }
    }
}