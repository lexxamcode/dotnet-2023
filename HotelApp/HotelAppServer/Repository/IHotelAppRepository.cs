using HotelDomain;

namespace HotelAppServer.Repository;
public interface IHotelAppRepository
{
    List<Booking> Bookings { get; }
    List<Client> Clients { get; }
    List<Hotel> Hotels { get; }
    List<Room> Rooms { get; }
}