namespace HotelAppServer.Dto;

/// <summary>
/// RoomPostDto for posting value with Room type
/// </summary>
public class RoomPostDto
{
    /// <summary>
    /// Type - string typed value representing a type of the room
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// Amount - uint value for storing an amount of rooms of this type
    /// </summary>
    public uint Amount { get; set; }
    /// <summary>
    /// Cost - uint for storing a cost of the room
    /// </summary>
    public uint Cost { get; set; }
}
