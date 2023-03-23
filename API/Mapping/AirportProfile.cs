using API.ApiResponses;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using System;

namespace API.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Order, DraftOrderResponse>()
                .ForCtorParam(nameof(DraftOrderResponse.OrderId), x => x.MapFrom(src => src.Id))
                .ForCtorParam(nameof(DraftOrderResponse.CustomerId), x => x.MapFrom(src => src.CustomerId))
                .ForCtorParam(nameof(DraftOrderResponse.CreatedDate), x => x.MapFrom(src => src.CreatedDate))
                .ForCtorParam(nameof(DraftOrderResponse.TotalPrice), x => x.MapFrom(src => src.TotalPrice));

            CreateMap<Order, ConfirmedOrderResponse>()
                .ForCtorParam(nameof(ConfirmedOrderResponse.OrderId), x => x.MapFrom(src => src.Id))
                .ForCtorParam(nameof(ConfirmedOrderResponse.CustomerId), x => x.MapFrom(src => src.CustomerId))
                .ForCtorParam(nameof(ConfirmedOrderResponse.ConfirmedDate), x => x.MapFrom(src => src.ConfirmedDate));

            CreateMap<Airport, AirportViewModel>();
        }
    }
}