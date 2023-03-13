using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightsContext _context;
    
    private IUnitOfWork UnitOfWork
    {
        get { return _context; }
    }

    public FlightRepository(FlightsContext context)
    {
            _context = context;
    }
    public Flight Add(Flight flight)
    {
        throw new NotImplementedException();
    }

    public void Update(Flight flight)
    {
        throw new NotImplementedException();
    }

    public async Task<Flight> GetAsync(Guid flightId)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Flight>> SearchAsync(Guid destinationAirportCode)
    {
        // Using Lazy loading
        var queryableResult = _context.Flights
            .Where(f => f.DestinationAirportId == destinationAirportCode)
            .AsQueryable();

        return queryableResult;
    }
}