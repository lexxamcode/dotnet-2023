using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
public class RoomType
{
    public string Type { get; set; }
    public uint Amount { get; set; }
    public uint Cost { get; set; }
    
    public RoomType(string type, uint amount, uint cost)
    {
        Type = type;
        Amount = amount;
        Cost = cost;
    }
}
