using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
public class BookedRoomType
{
    public RoomType Room { get; set; }
    public ClientType Client { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime BookingPeriod { get; set; }
    public DateTime DepartureDate { get; set; }

    public BookedRoomType(RoomType room, ClientType client, DateTime checkInDate, DateTime bookingPeriod, DateTime departureDate)
    {
        Room = room;
        Client = client;
        CheckInDate = checkInDate;
        BookingPeriod = bookingPeriod;
        DepartureDate = departureDate;
    }
}
