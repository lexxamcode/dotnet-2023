using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDomain;
/// <summary>
/// RoomType describes a room in hotel.
/// </summary>
[Table("room")]
public class Room
{
    /// <summary>
    /// Id - guid typed value for storing Id of the room
    /// </summary>
    [Column("id")]
    public uint Id { get; set; }
    /// <summary>
    /// Hotel Id where room is
    /// </summary>
    [Column("hotel_id")]
    [ForeignKey("hotel_id")]
    [Required]
    public uint? HotelId { get; set; }
    /// <summary>
    /// Type - string typed value representing a type of the room
    /// </summary>
    [Column("type")]
    [Required]
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// Amount - uint value for storing an amount of rooms of this type
    /// </summary>
    [Column("amount")]
    [Required]
    public uint Amount { get; set; }
    /// <summary>
    /// Cost - uint for storing a cost of the room
    /// </summary>
    [Column("cost")]
    [Required]
    public uint Cost { get; set; }
    /// <summary>
    /// List of booking of this room
    /// </summary>
    public List<Booking>? Bookings { get; set; }
    /// <summary>
    /// Default constructor
    /// </summary>
    public Room() { }
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="id"></param>
    /// <param name="hotelId"></param>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cost"></param>
    public Room(uint id, uint hotelId, string type, uint amount, uint cost)
    {
        Id = id;
        HotelId = hotelId;
        Type = type;
        Amount = amount;
        Cost = cost;
    }
    /// <summary>
    /// Constructor with bookings list
    /// </summary>
    /// <param name="id"></param>
    /// <param name="hotelId"></param>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="cost"></param>
    /// <param name="bookings"></param>
    public Room(uint id, uint hotelId, string type, uint amount, uint cost, List<Booking> bookings) : this(id, hotelId, type, amount, cost)
    {
        Bookings = bookings;
    }

    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool value representing are objects equal or not</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Room param)
            return false;
        return Id == param.Id && Type == param.Type && Amount == param.Amount && Cost == param.Cost;
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
