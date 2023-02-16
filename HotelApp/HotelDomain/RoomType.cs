using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
/// <summary>
/// RoomType describes a room in hotel.
/// </summary>
public class RoomType
{
    public string Type { get; set; } = string.Empty;
    public uint Amount { get; set; } = uint.MinValue;
    public uint Cost { get; set; } = uint.MinValue;

    public RoomType() { }
    public RoomType(string type, uint amount, uint cost)
    {
        Type = type;
        Amount = amount;
        Cost = cost;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not RoomType param)
            return false;
        return Type == param.Type && Amount == param.Amount && Cost == param.Cost;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Amount, Cost);
    }
}
