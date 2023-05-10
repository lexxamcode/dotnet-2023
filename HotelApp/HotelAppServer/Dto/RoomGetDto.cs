namespace HotelAppServer.Dto;

public class RoomGetDto
{
    /// <summary>
    /// Id - guid typed value for storing Id of the room
    /// </summary>
    public uint Id { get; set; }
    /// <summary>
    /// Hotel Id where room is
    /// </summary>
    public uint? HotelId { get; set; }
    /// <summary>
    /// Type - string typed value representing a type of the room
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// Amount - uint value for storing an amount of rooms of this type
    public uint Amount { get; set; }
    /// <summary>
    /// Cost - uint for storing a cost of the room
    /// </summary>
    public uint Cost { get; set; }
    /// <summary>
    /// List of booking of this room
    /// </summary>
}
