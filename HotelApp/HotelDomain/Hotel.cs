using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HotelDomain;

/// <summary>
/// Hotel is a type describing a hotel
/// </summary>
[Table("hotel")]
public class Hotel
{
    /// <summary>
    /// Id - uint typed value for storing Id of the client
    /// </summary>
    public uint Id { get; set; }
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
    /// <summary>
    /// Rooms - list of room of the hotel
    /// </summary>
    public List<Room>? Rooms { get; set; }
    /// <summary>
    /// Default constructor
    /// </summary>

    public Hotel() { }
    /// <summary>
    /// Constructor with parameters without collections
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="city"></param>
    /// <param name="address"></param>
    public Hotel(uint id, string name, string city, string address)
    {
        Id = id;
        Name = name;
        City = city;
        Address = address;
    }
    /// <summary>
    /// Constructor with full bunch of parameters, including collections
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="city"></param>
    /// <param name="address"></param>
    /// <param name="rooms"></param>
    public Hotel(uint id, string name, string city, string address, List<Room> rooms)
           : this(id, name, city, address)
    {
        Rooms = rooms;
    }
    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool value representing are objects equal or not</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Hotel param)
            return false;

        return Id == param.Id
            && Name == param.Name
            && City == param.City
            && Address == param.Address
            && Rooms.Equals(param.Rooms);
    }
    /// <summary>
    /// GetHashCode override
    /// </summary>
    /// <returns>Hash code of id</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
