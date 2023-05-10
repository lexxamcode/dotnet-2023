namespace HotelAppServer.Dto;

/// <summary>
/// HotelPostDto for adding Hotel typed values to repository
/// </summary>
public class HotelPostDto
{

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
