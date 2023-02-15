namespace HotelDomain;
public class HotelType
{ 
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int Id { get; set; }

    public HotelType(string name, string city, string address, int id)
    {
        Name = name;
        City = city;
        Address = address;
        Id = id;
    }
}
