using Domain.Aggregates.FlightAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class FlightRateAvailabilityChangedEventHandler : INotificationHandler<FlightRateAvailabilityChangedEvent>
    {
        private readonly IFlightRepository _flightRepository;
        public FlightRateAvailabilityChangedEventHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public Task Handle(FlightRateAvailabilityChangedEvent notification, CancellationToken cancellationToken)
        {
            notification.Flight.MutateRateAvailability(notification.FlightRate.Id, notification.Mutation);
            _flightRepository.Update(notification.Flight);
            return Task.CompletedTask;
        }
    }
}
