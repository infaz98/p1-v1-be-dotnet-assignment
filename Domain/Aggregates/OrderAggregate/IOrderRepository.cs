using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order);
        Task<Order> GetAsync(Guid orderId);
        void Update(Order order);
    }
}
