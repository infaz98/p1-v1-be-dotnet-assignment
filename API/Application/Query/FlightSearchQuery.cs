using System;
using System.Collections.Generic;
using API.Application.ViewModels;
using MediatR;

namespace API.Application.Query;

public class FlightSearchQuery : IRequest<List<FlightViewModel>>
{
    public Guid DestinationAirPortId { get; set; }
}