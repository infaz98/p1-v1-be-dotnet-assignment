using API.ApiResponses;
using API.Filter;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public class SearchFlightsQuery : IRequest<List<FlightResponse>>
    {
        public SearchFilter SearchFilter { get; set; }
    }
}
