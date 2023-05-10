using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDomain;
/// <summary>
/// BookedRoomType describes booked room in hotel
/// </summary>
public class Booking
{
    /// <summary>
    /// Id - uint typed value for storing Id of the booked room
    /// </summary>
    public uint Id { get; set; }
    /// <summary>
    /// Room id represents a type of the booked room
    /// </summary>
    [ForeignKey("RoomId")]
    public uint? RoomId { get; set; }
    /// <summary>
    /// Client value represents a person who booked the room
    /// </summary>
    [ForeignKey("ClientId")]
    public uint? ClientId { get; set; }
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
    /// <summary>
    /// Default constructor
    /// </summary>
    public Booking() { }
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="id"></param>
    /// <param name="roomId"></param>
    /// <param name="clientId"></param>
    /// <param name="checkInDate"></param>
    /// <param name="bookingPeriodInDays"></param>
    public Booking(uint id, uint roomId, uint clientId, DateTime checkInDate, uint bookingPeriodInDays)
    {
        Id = id;
        RoomId = roomId;
        ClientId = clientId;
        CheckInDate = checkInDate;
        BookingPeriodInDays = bookingPeriodInDays;
    }
    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool value representing are objects equal or not</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Booking param)
            return false;

        return Id == param.Id &&
               RoomId == param.RoomId &&
               ClientId == param.ClientId &&
               CheckInDate == param.CheckInDate &&
               DepartureDate == param.DepartureDate &&
               BookingPeriodInDays == param.BookingPeriodInDays;
    }
    /// <summary>
    /// GetHashCode override
    /// </summary>
    /// <returns>Hash code of id</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
