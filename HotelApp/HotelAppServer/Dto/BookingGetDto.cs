namespace HotelAppServer.Dto;

/// <summary>
/// BookingGetDto for getting Booking value from repository
/// </summary>
public class BookingGetDto
{
    /// <summary>
    /// Id - uint typed value for storing Id of the booked room
    /// </summary>
    public uint Id { get; set; }
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
    /// <summary>
    /// DepartureDate - DateTime typed value representing a departure date
    /// </summary>
    public DateTime DepartureDate { get => CheckInDate.AddDays(BookingPeriodInDays); }
}
