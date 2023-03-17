using MediatR;
using System;

namespace Domain.Events
{
    public class FlightBookingEvent : INotification
    {
        public int CustomerId { get; private set; }
        public Guid FlightRateId { get; private set; }
        public decimal FligtRatePrice { get; private set; }
        public int Quantity { get; private set; }

        public FlightBookingEvent(int customerId, Guid flightRateId, decimal flightRatePrice, int quantity)
        {
            CustomerId = customerId;
            FligtRatePrice = flightRatePrice;
            FlightRateId = flightRateId;
            Quantity = quantity;
        }
    }
}
