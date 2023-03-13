using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace API.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public Passenger Customer { get; set; }
        public Address CustomerAddress { get; set; }
        public Flight Flight { get; set; }
        public string Class { get; set; }
        public int NoOfPassengers {  get; set; }
    }
}
