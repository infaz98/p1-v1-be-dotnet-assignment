using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository
    {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetOrder(Guid orderId);
    }
}
