using API.ApiResponses;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public class CustomerBookingsQuery : IRequest<List<BookingHistoryResponse>>
    {
        public int CustomerId { get; set; }
    }
}
