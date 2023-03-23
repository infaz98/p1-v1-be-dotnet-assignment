using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateOrderDraftCommandHandler : IRequestHandler<CreateOrderDraftCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRepository _flightRepository;
        
        public CreateOrderDraftCommandHandler(
            IFlightRepository flightRepository,
            IOrderRepository orderRepository
            )
        {
            _orderRepository = orderRepository;
            _flightRepository = flightRepository;
        }
        public async Task<Order> Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.GetByFlightRateId(request.FlightRateId);

            if(flight == null)
            {
                throw new ArgumentException("Unable to find the Flight");
            }

            var flightRate = flight.Rates.FirstOrDefault(x => x.Id == request.FlightRateId);
           
            if (flightRate == null)
            {
                throw new ArgumentException("Unable to find the Flight Rate");
            }

            var order = new Order(request.CustomerId, flightRate.Id, flightRate.Price.Value, request.Quantity);
            _orderRepository.Add(order);
             
            await _flightRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return order;
        }
    }
}
