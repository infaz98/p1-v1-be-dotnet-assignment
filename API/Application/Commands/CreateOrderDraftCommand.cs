using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class CreateOrderDraftCommand : IRequest<Order>
    {
        public int CustomerId { get; private set; }
        public Guid FlightRateId { get; private set; }
        public int Quantity { get; private set; }

        public CreateOrderDraftCommand(int customerId, Guid flightRateId, int quantity)
        {
            CustomerId = customerId;
            FlightRateId = flightRateId;
            Quantity = quantity;
        }
    }
}