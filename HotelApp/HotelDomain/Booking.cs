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
    /// Room value represents a type of the booked room
    /// </summary>
    public Room Room { get; set; } = new();
    /// <summary>
    /// Client value represents a person who booked the room
    /// </summary>
    public Client Client { get; set; } = new();
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

    public Booking() { }
    public Booking(uint id, Room room, Client client, DateTime checkInDate, uint bookingPeriodInDays)
    {
        Id = id;
        Room = room;
        Client = client;
        CheckInDate = checkInDate;
        BookingPeriodInDays = bookingPeriodInDays;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Booking param)
            return false;

        return Id == param.Id &&
               Room.Equals(param.Room) && Client.Equals(param.Client) &&
               CheckInDate == param.CheckInDate &&
               DepartureDate == param.DepartureDate &&
               BookingPeriodInDays == param.BookingPeriodInDays;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
