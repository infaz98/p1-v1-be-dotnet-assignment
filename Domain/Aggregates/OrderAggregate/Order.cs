using Domain.Aggregates.FlightAggregate;
using Domain.Exceptions;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Guid FlightId { get; private set; }
        
        public DateTimeOffset OrderDate { get; private set;  } = DateTimeOffset.Now;
        public int NoOfSeats {  get; private set; }
        public string ClassName {  get; private set; }

        public Address Address { get; private set; }
        
        private List<Flight> _flight;
        private List<Passenger> _passengers;
        
        public IReadOnlyCollection<Flight> flight => _flight;
        public IReadOnlyCollection<Passenger> Passengers => _passengers;
        
        private Order()
        {
            _passengers = new List<Passenger>();
        }

        public Order(Guid flightId, int noOfSeats, string className) : this()
        {
            FlightId = flightId;
            NoOfSeats = noOfSeats;
            ClassName = className;
        }

        public void AddPassengers(string name, int age, string email)
        {
             var passenger = new Passenger(name, age, email);
            _passengers.Add(passenger);
        }   

        public void AddOrder(Guid flightId, int noOfSeats, string serviceClass)
        {
            var bookedFlight = GetFlight(flightId, serviceClass);
            
        }

        private decimal CalculateRate(decimal rate, int noOfSeats)
        {
            return rate * noOfSeats;
        }
        private Flight GetFlight (Guid flightId, string serviceClass)
        {
            var bookedFlight = _flight.SingleOrDefault(f => f.Id == flightId); 

            if (bookedFlight == null)
            {
                throw new ArgumentException("This flight does exists in the provided flightId");
            }

            return bookedFlight;
        }
    }
}
