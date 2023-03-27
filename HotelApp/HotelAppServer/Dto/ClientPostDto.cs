namespace HotelAppServer.Dto;

/// <summary>
/// Client class without Id
/// </summary>
public class ClientPostDto
{
    /// <summary>
    /// Passport - a string representing passport number
    /// </summary>
    public string Passport { set; get; } = string.Empty;
    /// <summary>
    /// FirstName - a string value representing person's name
    /// </summary>  
    public string FirstName { set; get; } = string.Empty;
    /// <summary>
    /// LastName - a string value representing person's last name
    /// </summary> 
    public string LastName { set; get; } = string.Empty;
    /// <summary>
    /// Surname - a string value representing person's surname
    /// </summary> 
    public string Surname { set; get; } = string.Empty;
    /// <summary>
    /// BirthDate - DateTime typed value for storing birth date of person
    /// </summary>
    public DateTime BirthDate { set; get; } = DateTime.MinValue;
}
