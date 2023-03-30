using HotelDomain;

namespace HotelAppServer.Dto;

/// <summary>
/// BookingPostDto for posting Booking type
/// </summary>
public class BookingPostDto
{
    /// <summary>
    /// RoomId value represents an id of type of the booked room
    /// </summary>
    public uint RoomId { get; set; } = new();
    /// <summary>
    /// ClientId value represents a id of person who booked the room
    /// </summary>
    public uint ClientId { get; set; } = new();
    /// <summary>
    /// CheckInDate - DateTime typed value for storing a date of checking-in
    /// </summary>
    public DateTime CheckInDate { get; set; } = DateTime.MinValue;
    /// <summary>
    /// BookingPeriodInDays double typed value representing an amount of days between check-in and departure
    /// </summary>
    public uint BookingPeriodInDays { get; set; }
}
