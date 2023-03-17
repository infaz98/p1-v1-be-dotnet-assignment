using API.ApiResponses;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<bool>
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
