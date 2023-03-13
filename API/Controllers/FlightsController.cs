using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.ApiResponses;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;
using API.Application.Query;
using API.Application.ViewModels;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;
    private readonly IMediator _mediator;
    private readonly FlightSearchQuery _flightSearchQuery;

    public FlightsController(ILogger<FlightsController> logger, FlightSearchQuery flightSearchQuery, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _flightSearchQuery = flightSearchQuery;
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> GetAvailableFlights(Guid destinationAirPortId)
    {
        try
        {
            _flightSearchQuery.DestinationAirPortId = destinationAirPortId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Some thing went wrong.");
            
            return BadRequest();
        }

        return Ok(await _mediator.Send(_flightSearchQuery));
    }
}