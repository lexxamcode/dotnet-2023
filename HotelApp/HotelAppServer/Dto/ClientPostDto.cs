namespace HotelAppServer.Dto;

public class ClientPostDto
{
    /// <summary>
    /// Passport - a string representing passport number
    /// </summary>
    public string Passport { set; get; } = string.Empty;
    /// <summary>
    /// FirstName, LastName, Surname - a string for name, last name and surname optionally
    /// </summary>  
    public string FirstName { set; get; } = string.Empty;
    public string LastName { set; get; } = string.Empty;
    public string Surname { set; get; } = string.Empty;
    /// <summary>
    /// BirthDate - DateTime typed value for storing birth date of person
    /// </summary>
    public DateTime BirthDate { set; get; } = DateTime.MinValue;

    }
