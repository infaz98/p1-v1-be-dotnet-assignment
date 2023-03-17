using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRepository : IFlightRepository
    {

        private readonly FlightsContext _context;

        public FlightRepository(FlightsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public Flight Add(Flight flight)
        {
            return _context.Flights.Add(flight).Entity;
        }

        public async Task<Flight> GetAsync(Guid flightId)
        {
            return await _context.Flights.Include(o => o.Rates).FirstOrDefaultAsync(o => o.Id == flightId);
        }

        public async Task<Flight> GetByFlightRateId(Guid flightId)
        {
            return await _context.Flights.Include(r => r.Rates)
                .Where(f => f.Rates.Any(r => r.Id == flightId))
                .FirstOrDefaultAsync();
        }

        public void Update(Flight flight)
        {
            _context.Flights.Update(flight);
        }
    }
}
