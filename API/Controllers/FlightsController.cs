using System.Net;
using System.Threading.Tasks;
using API.ApiResponses;
using API.Application.Commands;
using API.Application.Queries;
using API.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// returns the flight details for flights going to a specific destination, 
    /// sorted by lowest available price and limited to a specific page size with a specified offset.
    /// </summary>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> GetAvailableFlights([FromQuery] SearchFilter searchFilter)
    {
        var searchResult = await _mediator.Send(new SearchFlightsQuery()
        {
            SearchFilter = searchFilter
        });

        if(searchResult.Count == 0)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Data = searchResult
            });
        }
        else
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = searchResult
            });
        }
    }

    [HttpGet]
    [Route("Customer-Orders/{customerId}")]
    public async Task<IActionResult> GetCustomerOrders(int customerId)
    {
        var customerOrders = await _mediator.Send(new CustomerBookingsQuery { CustomerId = customerId });

        if (customerOrders == null || customerOrders.Count == 0)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = customerOrders
            });
        }

        return Ok(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Data = customerOrders
        });
    }

    [HttpPost]
    [Route("Book")]
    public async Task<IActionResult> FlightBook([FromBody] CreateOrderDraftCommand createOrderDraftCommand)
    {
        var response = await _mediator.Send(createOrderDraftCommand);
        if (response)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Booking is created successfully - Confirm to proceed futher :)"
            });
        }

        return Ok(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Unable to create the booking :("
        });
    }

    [HttpPut]
    [Route("Confirm-Booking")]
    public async Task<IActionResult> ConfirmFlightBooking([FromBody] ConfirmOrderCommand confirmOrderCommand)
    {
        var response = await _mediator.Send(confirmOrderCommand);
        if (response)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = "Booking is confirmed successfully :)"
            });
        }
        return Ok(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Unable to confirm the booking :("
        });
    }
}
