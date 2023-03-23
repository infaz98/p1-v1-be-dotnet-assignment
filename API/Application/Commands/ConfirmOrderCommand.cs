using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<Order>
    {
        public int CustomerId { get; set; }
        public Guid OrderId { get; set; }

        public ConfirmOrderCommand(int customerId, Guid orderId)
        {
          OrderId = orderId;
          CustomerId = customerId;
        }
    }
}
