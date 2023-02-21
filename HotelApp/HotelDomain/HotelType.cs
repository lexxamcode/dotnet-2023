using HotelDomain;
namespace HotelDomain;

/// <summary>
/// HotelType is a type describing a hotel
/// </summary>
public class HotelType
{
    /// <summary>
    /// Name - string value for name of the hotel
    /// </summary>
    public string Name { get; set; } = String.Empty;
    /// <summary>
    /// City - string value for city name of the hotel
    /// </summary>
    public string City { get; set; } = String.Empty;    
    /// <summary>
    /// Address - string value for address where the hotel is
    /// </summary>
    public string Address { get; set; } = String.Empty; 

    public List<RoomType> Rooms { get; set; } = new();
    public List<BookedRoomType> BookedRooms { get; set; } = new();
    public List<ClientType> Clients { get; set; } = new();

    public HotelType() { }
    public HotelType(string name, string city, string address)
    {
        Name = name;
        City = city;
        Address = address;
    }

    public HotelType(string name, string city, string address, List<RoomType> rooms, List<BookedRoomType> bookedRooms, List<ClientType> clients)
           : this(name, city, address)
    {
        Rooms = rooms;
        BookedRooms = bookedRooms;
        Clients = clients;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not HotelType param)
            return false;

        return Name == param.Name && City == param.City &&
               Address == param.Address && Rooms.Equals(param.Rooms) &&
               BookedRooms.Equals(param.BookedRooms) && Clients.Equals(param.Clients);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name,City, Address, Rooms, BookedRooms, Clients);
    }
}
