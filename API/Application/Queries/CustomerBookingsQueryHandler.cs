using API.ApiResponses;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class CustomerBookingsQueryHandler : IRequestHandler<CustomerBookingsQuery, List<BookingHistoryResponse>>
    {
        private readonly FlightsContext _flightsContext;

        public CustomerBookingsQueryHandler(FlightsContext flightsContext)
        {
            _flightsContext = flightsContext;
        }
        public Task<List<BookingHistoryResponse>> Handle(CustomerBookingsQuery request, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT o.""Id"" AS OrderId, o.""TotalPrice"", o.""Quantity"", f.""Departure"", f.""Arrival"", 
                    origin.""Code"" AS OriginAirportCode, dest.""Code"" AS DestinationAirportCode,
                    CASE WHEN o.""IsDraft"" = false THEN 'Booked' ELSE 'Not confirmed' END AS BookingStatus
                FROM public.""Orders"" o
                JOIN public.""FlightRates"" fr ON o.""FlightRateId"" = fr.""Id""
                JOIN public.""Flights"" f ON fr.""FlightId"" = f.""Id""
                JOIN public.""Airports"" origin ON f.""OriginAirportId"" = origin.""Id""
                JOIN public.""Airports"" dest ON f.""DestinationAirportId"" = dest.""Id""
                WHERE o.""CustomerId"" = @CustomerId";



            // Create and add the parameters for the SQL query
            var parameters = new List<object>
            {
                new Npgsql.NpgsqlParameter("@CustomerId", request.CustomerId)
            };



            using (var command = _flightsContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters.ToArray());

                _flightsContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<BookingHistoryResponse>();

                    while (result.Read())
                    {
                        entities.Add(new BookingHistoryResponse
                            (
                            result.GetGuid(result.GetOrdinal("OrderId")),
                            result.GetString(result.GetOrdinal("BookingStatus")),
                            result.GetDecimal(result.GetOrdinal("TotalPrice")),
                            result.GetInt32(result.GetOrdinal("Quantity")),
                            result.GetDateTime(result.GetOrdinal("Departure")),
                            result.GetDateTime(result.GetOrdinal("Arrival")),
                            result.GetString(result.GetOrdinal("OriginAirportCode")),
                            result.GetString(result.GetOrdinal("DestinationAirportCode"))
                            )
                        );
                    }

                    return Task.FromResult(entities);
                }
            }
        }
    }
}
