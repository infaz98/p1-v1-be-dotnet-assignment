using System;

namespace API.Application.ViewModels;

public class FlightViewModel
{
    public string DepartureAirportCode { get; set; }
    public string ArrivalAirportCode { get; set; }
    public DateTimeOffset DepartureDate { get; set; }
    public DateTimeOffset ArrivalDateTime { get; set; }
    public decimal LowestPrice { get; set; }
}