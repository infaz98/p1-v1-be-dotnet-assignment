using Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class FlightBookingEventHandler : INotificationHandler<FlightBookingEvent>
    {
        private readonly IOrderRepository _orderRepository;
        public FlightBookingEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Handle(FlightBookingEvent notification, CancellationToken cancellationToken)
        {
            var order = new Order(notification.CustomerId, notification.FlightRateId, notification.FligtRatePrice, notification.Quantity);
            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
