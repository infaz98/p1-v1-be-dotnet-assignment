using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRepository
    {
        Flight Add(Flight flight);

        void Update(Flight flight);

        Task<Flight> GetAsync(Guid flightId);
        
        Task<IQueryable<Flight>> SearchAsync(Guid destinationAirportCode);
    }
}