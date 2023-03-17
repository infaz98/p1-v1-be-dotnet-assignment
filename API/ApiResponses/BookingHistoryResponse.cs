using System;

namespace API.ApiResponses;

public record BookingHistoryResponse(Guid OrderId, string BookingStatus, decimal TotalPrice, int Quantity, DateTimeOffset Departure, DateTimeOffset Arrival, string OriginAirportCode, string DestinationAirportCode);
