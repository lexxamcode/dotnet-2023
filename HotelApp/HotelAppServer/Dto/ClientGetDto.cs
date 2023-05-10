using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAppServer.Dto;

/// <summary>
/// Client class without bookings list
/// </summary>
public class ClientGetDto
{
    /// <summary>
    /// Id - uint typed value for storing Id of the client
    /// </summary>
    public uint Id { get; set; }
    /// <summary>
    /// Passport - a string representing passport number
    /// </summary>
    public string Passport { set; get; } = string.Empty;
    /// <summary>
    /// FirstName - a string representing person's name
    /// </summary>  
    public string FirstName { set; get; } = string.Empty;
    /// <summary>
    /// LastName - a string representing person's last name
    /// </summary>
    public string LastName { set; get; } = string.Empty;
    /// <summary>
    /// Surname - a string representing person's surname
    /// </summary>
    public string Surname { set; get; } = string.Empty;
    /// <summary>
    /// BirthDate - DateTime typed value for storing birth date of person
    /// </summary>
    public DateTime BirthDate { set; get; } = DateTime.MinValue;
    /// <summary>
    /// List of clients bookings
    /// </summary>
}
