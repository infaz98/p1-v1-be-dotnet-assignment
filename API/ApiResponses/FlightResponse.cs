using System;

namespace API.ApiResponses;

public record FlightResponse(Guid FlightRateId, string DepartureAirportCode, string ArrivalAirportCode, DateTimeOffset Departure, DateTimeOffset Arrival, decimal PriceFrom);