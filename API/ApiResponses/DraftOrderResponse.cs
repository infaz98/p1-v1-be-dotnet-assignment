using System;

namespace API.ApiResponses;

public record DraftOrderResponse(Guid OrderId, int CustomerId, DateTimeOffset CreatedDate, decimal TotalPrice);