using API.Application.ViewModels;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Query;

public class FlightSearchQueryHandler : IRequestHandler<FlightSearchQuery, List<FlightViewModel>>
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;

    public FlightSearchQueryHandler(IFlightRepository flightRepository, IAirportRepository aircraftRepository)
    {
        _flightRepository = flightRepository;
        _airportRepository = aircraftRepository;
    }

    public async Task<List<FlightViewModel>> Handle(FlightSearchQuery request, CancellationToken cancellationToken)
    {
        // Filter out flights that doesn't  have price
        var flights = await _flightRepository.SearchAsync(request.DestinationAirPortId);

        // Get destination (arrival) airport code
        var destinationAirport = await _airportRepository.GetAsync(request.DestinationAirPortId);

        var view = new List<FlightViewModel>();

        foreach (var flight in flights)
        {
            var deptAirport = await _airportRepository.GetAsync(flight.DestinationAirportId);

            var lowestRate = flight.Rates.Where(n => n.Price.Value != 0).OrderBy(x => x.Price).FirstOrDefault();

            var searchResult = new FlightViewModel
            {
                ArrivalAirportCode = destinationAirport.Code,
                DepartureAirportCode = deptAirport.Code,
                DepartureDate = flight.Departure,
                ArrivalDateTime = flight.Arrival,
                LowestPrice = lowestRate.Price.Value
            };

            view.Add(searchResult);
        }


        return view;
    }
}