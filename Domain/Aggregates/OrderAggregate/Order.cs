using Domain.Exceptions;
using Domain.SeedWork;
using System;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public int CustomerId { get; private set; }
        public Guid FlightRateId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public bool IsDraft { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ConfirmedDate { get; private set; }

        protected Order()
        {
        }

        public Order(int customerId, Guid flightRateId, decimal flightRatePrice, int quantity) : this()
        {
            if (quantity < 1)
            {
                throw new OrderDomainException("Quantity should be greater than or equal to 1.");
            }

            if (flightRatePrice < 1)
            {
                throw new OrderDomainException("Flight rate price should be greater than or equal to 1.");
            }

            CreatedDate = DateTime.Now;

            Quantity = quantity;
            TotalPrice = flightRatePrice * quantity;

            CustomerId = customerId;
            FlightRateId = flightRateId;
            
            IsDraft = true;
        }

        //Not allow to update if already order is confirmed 
        public void Confirm()
        {
            if (!IsDraft)
            {
                throw new OrderDomainException("This order has already been confirmed.");
            }

            IsDraft = false;
            ConfirmedDate = DateTime.Now;
        }
    }
}