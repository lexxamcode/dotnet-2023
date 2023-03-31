namespace HotelAppServer.Dto;

/// <summary>
/// HotelGetDto for getting Hotel value from repository
/// </summary>
public class HotelGetDto
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
}
