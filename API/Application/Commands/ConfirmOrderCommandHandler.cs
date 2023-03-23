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
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRepository _flightRepository;

        public ConfirmOrderCommandHandler(
            IOrderRepository orderRepository,
            IFlightRepository flightRepository
            )
        {
            _orderRepository = orderRepository;
            _flightRepository = flightRepository;
        }

        public async Task<Order> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order  = await _orderRepository.GetAsync(request.OrderId);

            if (order == null)
            {
                throw new ArgumentException("Unable to find the Order");
            }

            if(order.CustomerId != request.CustomerId)
            {
                throw new ArgumentException("Unable to process the Order");
            }

            var flight = await _flightRepository.GetByFlightRateId(order.FlightRateId);
            var flightRate = flight.Rates.FirstOrDefault(r => r.Id == order.FlightRateId);
           
                if (flight == null || flightRate == null)
            {
                throw new ArgumentException("Unable to process the order");
            }

            order.Confirm();
            order.AddDomainEvent(new FlightRateAvailabilityChangedEvent(flight, flightRate, -order.Quantity));
            
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return order;
        }
    }
}
