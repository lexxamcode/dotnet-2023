namespace HotelDomain;
/// <summary>
/// RoomType describes a room in hotel.
/// </summary>
public class Room
{
    /// <summary>
    /// Type - string typed value representing a type of the room
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// Amount - uint value for storing an amount of rooms of this type
    /// </summary>
    public uint Amount { get; set; } = uint.MinValue;
    /// <summary>
    /// Cost - uint for storing a cost of the room
    /// </summary>
    public uint Cost { get; set; } = uint.MinValue;

    public Room() { }
    public Room(string type, uint amount, uint cost)
    {
        Type = type;
        Amount = amount;
        Cost = cost;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Room param)
            return false;
        return Type == param.Type && Amount == param.Amount && Cost == param.Cost;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Amount, Cost);
    }
}
