namespace HotelDomain;
/// <summary>
/// ClientType describes a person registered at hotel
/// </summary>
public class Client
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
    /// FirstName, LastName, LastName - a string for name, last name and surname optionally
    /// </summary>  
    public string FirstName { set; get; } = string.Empty;
    public string LastName { set; get; } = string.Empty;
    public string Surname { set; get; } = string.Empty;
    /// <summary>
    /// BirthDate - DateTime typed value for storing birth date of person
    /// </summary>
    public DateTime BirthDate { set; get; } = DateTime.MinValue;

    public Client() { }
    public Client(uint id, string passport, string firstName, string lastName, string surname, DateTime birthDate)
    {
        Id = id;
        Passport = passport;
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
        BirthDate = birthDate;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Client param)
            return false;

        return Id == param.Id &&
               Passport == param.Passport &&
               FirstName == param.FirstName &&
               LastName == param.LastName &&
               Surname == param.Surname &&
               BirthDate == param.BirthDate;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
