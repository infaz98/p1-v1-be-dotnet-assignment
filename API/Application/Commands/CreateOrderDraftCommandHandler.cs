using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateOrderDraftCommandHandler : IRequestHandler<CreateOrderDraftCommand, bool>
    {
        private readonly IFlightRepository _flightRepository;

        public CreateOrderDraftCommandHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public async Task<bool> Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.GetByFlightRateId(request.FlightRateId);

            if(flight == null)
            {
                throw new ArgumentException("Unable to find the Flight");
            }

            var flightRate = flight.Rates.FirstOrDefault(x => x.Id == request.FlightRateId);

            flight.AddDomainEvent(new FlightBookingEvent(request.CustomerId, flightRate.Id, flightRate.Price.Value, request.Quantity));
            return await _flightRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
