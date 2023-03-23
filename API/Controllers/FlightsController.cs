using System.Net;
using System.Threading.Tasks;
using API.ApiResponses;
using API.Application.Commands;
using API.Application.Queries;
using API.Filter;
using AutoMapper;
using Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public FlightsController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Search flight by destination
    /// </summary>
    /// <remarks>
    /// Returns the flight details for flights going to a specific destination,
    /// sorted by lowest available price and limited to a specific page size with a specified offset.
    /// 
    /// Try out a destination like "Istanbul"
    /// </remarks>
    /// <param name="searchFilter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetAvailableFlights([FromQuery] SearchFilter searchFilter)
    {
        var searchResult = await _mediator.Send(new SearchFlightsQuery()
        {
            SearchFilter = searchFilter
        });

        if (searchResult.Count == 0)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = $"Couldn't find any flight destionations that contains {searchFilter.Pharam} as name",
                Data = searchResult
            });
        }
        else
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = $"Flight destionations that contains {searchFilter.Pharam} as name",
                Data = searchResult
            });
        }
    }

    /// <summary>
    /// Retrieve bookings for given customer
    /// </summary>
    /// <remarks>
    /// Returns all the confirmed and not confirmed booking for a given customer by customer Id 
    /// </remarks>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Customer-Orders/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
    public async Task<IActionResult> GetCustomerOrders(int customerId)
    {
        var customerOrders = await _mediator.Send(new CustomerBookingsQuery { CustomerId = customerId });

        if (customerOrders == null || customerOrders.Count == 0)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = $"No orders for the customer - {customerId}",
                Data = customerOrders
            });
        }

        return Ok(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = $"Available orders for the customer - {customerId}",
            Data = customerOrders
        });
    }

    /// <summary>
    /// Book a Flight
    /// </summary>
    /// <remarks>
    /// Returns a created status with booking information for successful booking
    /// </remarks>
    /// <param name="createOrderDraftCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Book")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse))]
    public async Task<IActionResult> FlightBook([FromBody] CreateOrderDraftCommand createOrderDraftCommand)
    {
        var order = await _mediator.Send(createOrderDraftCommand);
        
        if (order != null)
        {
            var orderDraftResponse = _mapper.Map<DraftOrderResponse>(order);
            return CreatedAtAction(actionName: nameof(GetCustomerOrders), routeValues: new { customerId = order.CustomerId }, value: orderDraftResponse);
        }

        return BadRequest(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Unable to create the booking :("
        });
    }

    /// <summary>
    /// Confirming a booked flight
    /// </summary>
    /// <remarks>
    /// Updates the booking status to confirmed for the given order id   
    /// </remarks>
    /// <param name="confirmOrderCommand"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Confirm-Booking")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
    public async Task<IActionResult> ConfirmFlightBooking([FromBody] ConfirmOrderCommand confirmOrderCommand)
    {
        var order = await _mediator.Send(confirmOrderCommand);
        if (order != null)
        {
            return Ok(new BaseResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Booking is confirmed successfully :)",
                Data = _mapper.Map<ConfirmedOrderResponse>(order)
            });
        }
        return Ok(new BaseResponse()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Unable to confirm the booking :("
        });
    }
}
