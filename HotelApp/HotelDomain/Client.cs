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
    /// Default constructor
    /// </summary>
    public Client() { }
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="id"></param>
    /// <param name="passport"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="surname"></param>
    /// <param name="birthDate"></param>
    public Client(uint id, string passport, string firstName, string lastName, string surname, DateTime birthDate)
    {
        Id = id;
        Passport = passport;
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
        BirthDate = birthDate;
    }
    /// <summary>
    /// Equals override
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>bool value representing are objects equal or not</returns>
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
    /// <summary>
    /// GetHashCode override
    /// </summary>
    /// <returns>Hash code of Id</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
