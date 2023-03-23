using System;

namespace API.ApiResponses;

public record ConfirmedOrderResponse(Guid OrderId, int CustomerId, DateTimeOffset ConfirmedDate);