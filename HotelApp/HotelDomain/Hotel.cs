namespace HotelDomain;

/// <summary>
/// Hotel is a type describing a hotel
/// </summary>
public class Hotel
{
    /// <summary>
    /// Id - guid typed value for storing Id of the client
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
    /// Name - string value for name of the hotel
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// City - string value for city name of the hotel
    /// </summary>
    public string City { get; set; } = string.Empty;
    /// <summary>
    /// Address - string value for address where the hotel is
    /// </summary>
    public string Address { get; set; } = string.Empty;

    public List<Room> Rooms { get; set; } = new();
    public List<BookedRoom> BookedRooms { get; set; } = new();
    public List<Client> Clients { get; set; } = new();

    public Hotel() { }
    public Hotel(Guid id, string name, string city, string address)
    {
        Id = id;
        Name = name;
        City = city;
        Address = address;
    }

    public Hotel(Guid id, string name, string city, string address, List<Room> rooms, List<BookedRoom> bookedRooms, List<Client> clients)
           : this(id, name, city, address)
    {
        Rooms = rooms;
        BookedRooms = bookedRooms;
        Clients = clients;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Hotel param)
            return false;

        return Id == param.Id && Name == param.Name &&
               City == param.City && Address == param.Address &&
               Rooms.Equals(param.Rooms) && BookedRooms.Equals(param.BookedRooms) &&
               Clients.Equals(param.Clients);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, City, Address, Rooms, BookedRooms, Clients);
    }
}
