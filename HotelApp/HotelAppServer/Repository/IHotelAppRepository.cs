using HotelDomain;

namespace HotelAppServer.Repository;
/// <summary>
/// Hotel Domain Repository Interface
/// </summary>
public interface IHotelAppRepository
{
    /// <summary>
    /// List of all bookings of all the hotels
    /// </summary>
    List<Booking> Bookings { get; }
    /// <summary>
    /// List of clients
    /// </summary>
    List<Client> Clients { get; }
    /// <summary>
    /// List of Hotels
    /// </summary>
    List<Hotel> Hotels { get; }
    /// <summary>
    /// List of all rooms of all the hotels
    /// </summary>
    List<Room> Rooms { get; }
}