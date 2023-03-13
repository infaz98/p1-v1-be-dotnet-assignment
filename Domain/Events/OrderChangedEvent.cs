using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.OrderAggregate;

namespace Domain.Events
{
    public class OrderChangedEvent : INotification
    {
        public Order Order { get; private set; }
        
        public OrderChangedEvent(Order order)
        {
            Order = order;
        }
    }
}
