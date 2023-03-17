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
    public class SearchFlightsQueryHandler : IRequestHandler<SearchFlightsQuery, List<FlightResponse>>
    {
        private readonly FlightsContext _flightsContext;

        public SearchFlightsQueryHandler(FlightsContext flightsContext)
        {
            _flightsContext = flightsContext;
        }

        public Task<List<FlightResponse>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
        {
            var destinationName = request.SearchFilter.Pharam;
            var pageIndex = request.SearchFilter.PageNumber;
            var pageSize = request.SearchFilter.PageSize;

            var sql = @"
                SELECT 
                    f.""Id"", 
                    fr.""Id"" AS FlightRateId,
                    f.""OriginAirportId"", 
                    f.""DestinationAirportId"", 
                    f.""Departure"", 
                    f.""Arrival"", 
                    MIN(fr.""Price_Value"") AS LowestPrice,
                    oac.""Code"" AS OriginAirportCode,
                    dac.""Code"" AS DestinationAirportCode
                FROM 
                    public.""Flights"" f
                    INNER JOIN public.""FlightRates"" fr ON f.""Id"" = fr.""FlightId""
                    INNER JOIN public.""Airports"" a ON f.""DestinationAirportId"" = a.""Id""
                    INNER JOIN public.""Airports"" oac ON f.""OriginAirportId"" = oac.""Id""
                    INNER JOIN public.""Airports"" dac ON f.""DestinationAirportId"" = dac.""Id""
                WHERE 
                    a.""Name"" LIKE @DestinationName
                    AND fr.""Available"" > 0
                GROUP BY 
                    f.""Id"", 
                    fr.""Id"", 
                    f.""OriginAirportId"", 
                    f.""DestinationAirportId"", 
                    f.""Departure"", 
                    f.""Arrival"", 
                    oac.""Code"", 
                    dac.""Code""
                ORDER BY 
                    LowestPrice
                OFFSET 
                    @Offset ROWS
                FETCH NEXT 
                    @PageSize ROWS ONLY";


            // Create and add the parameters for the SQL query
            var parameters = new List<object>
            {
                new Npgsql.NpgsqlParameter("@DestinationName", $"%{destinationName}%"),
                new Npgsql.NpgsqlParameter("@Offset", (pageIndex - 1) * pageSize),
                new Npgsql.NpgsqlParameter("@PageSize", pageSize)
            };


            using (var command = _flightsContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters.ToArray());

                _flightsContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<FlightResponse>();

                    while (result.Read())
                    {
                        entities.Add(new FlightResponse
                            (
                            result.GetGuid(result.GetOrdinal("FlightRateId")),
                            result.GetString(result.GetOrdinal("OriginAirportCode")),
                            result.GetString(result.GetOrdinal("DestinationAirportCode")),
                            result.GetDateTime(result.GetOrdinal("Departure")),
                            result.GetDateTime(result.GetOrdinal("Arrival")),
                            result.GetDecimal(result.GetOrdinal("LowestPrice"))
                            )
                        );
                    }

                    return Task.FromResult(entities);
                }
            }
        }
    }
}




