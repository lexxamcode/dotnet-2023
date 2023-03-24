using HotelDomain;

namespace UnitTest;

public class HotelDomainTest
{
    private readonly List<Hotel> _hotels;
    private readonly List<Client> _clients;

    public HotelDomainTest()
    {
        _hotels = TestDataRepository.GetHotelList();
        _clients = TestDataRepository.GetClientList();
    }

    /// <summary>
    /// Output information about all hotels in database as unit test
    /// </summary>
    [Fact]
    public void AllHotelsTest()
    {
        var requestHotelList = (from hotel in _hotels
                                select hotel).ToList();

        Assert.Equal("Sleepy Place", requestHotelList[0].Name);
        Assert.Equal("Voidburg", requestHotelList[1].City);
        Assert.Equal("Salzburg st. 3", requestHotelList[2].Address);
        Assert.Equal("Default", requestHotelList[3].Rooms[1].Type);
        Assert.Empty(requestHotelList[3].Bookings);
        Assert.NotEmpty(requestHotelList[4].Bookings);
    }

    /// <summary>
    /// Output all information about hotel clients as unit test
    /// </summary>
    [Fact]
    public void HotelClientsTest()
    {
        var hotelList = _hotels;

        var clients = (from hotel in hotelList
                       where hotel.Name == "Sleepy Place"
                       from booking in hotel.Bookings
                       orderby booking.Client.SecondName
                       select booking.Client).ToList();

        Assert.Equal("Anantha", clients[0].SecondName);
        Assert.Equal("09 87 654321", clients[1].Passport);
        Assert.Equal(DateTime.Parse("11.12.2003"), clients[2].BirthDate);
        Assert.Equal("Alexey", clients[3].FirstName);
        Assert.Equal(string.Empty, clients[4].Surname);
    }

    /// <summary>
    /// Top-5 most booked hotels as unit test
    /// </summary>
    [Fact]
    public void TopHotels()
    {
        var hotelList = _hotels;

        // Sort hotelList by booked rooms count using LINQ
        var linqSortedHotelList = (from hotel in hotelList
                                   orderby hotel.Bookings.Count descending
                                   select hotel).ToList();

        Assert.Equal("Chillzone", linqSortedHotelList[0].Name);       // "Chillzone" has 6 booked rooms
        Assert.Equal("Comfort Palace", linqSortedHotelList[1].Name);  // "Comfort" Palace has 3 booked rooms
        Assert.Equal("Sleepy Place", linqSortedHotelList[2].Name);    // "Sleepy" Place has 2 booked rooms
        Assert.Equal("First Class", linqSortedHotelList[3].Name);     // "First" class has 1 booked room
        Assert.Equal("Cheap'n'Cool", linqSortedHotelList[4].Name);    // "Cheap'n'cool" has no booked rooms
    }

    /// <summary>
    /// Output information about all available rooms at all hotels in one city as unit test
    /// 
    /// Each hotel in this test has 5 luxe rooms and 100 default rooms
    ///
    /// "Sleepy Place"       hotelList[0] has 2 booked default rooms                        => available: 5 luxe, 98 default
    /// "Comfort Palace"     hotelList[1] has 3 booked rooms: 1 luxe and 2 default rooms    => available: 4 luxe, 98 default
    /// "Chillzone"          hotelList[2] has 6 booked rooms: 2 luxes and 4 default rooms   => available: 3 luxe, 96 default
    /// "Cheap'n'Cool"       hotelList[3] has no booked rooms                               => available: 5 luxe, 100 default
    /// "First Class"        hotelList[4] has 1 booked default room                         => available: 5 luxe, 99 default 
    /// </summary>
    [Fact]
    public void AvailableRooms()
    {
        var freeRooms = (from hotel in _hotels
                         where hotel.City == "Voidburg"
                         from room in hotel.Rooms
                         select new
                         {
                             Hotel = hotel.Name,
                             Type = room.Type,
                             Amount = room.Amount - (from bookedRoom in hotel.Bookings
                                                     where bookedRoom.Room.Equals(room)
                                                     select bookedRoom).Count()
                         }).ToList();

        Assert.Equal(5, freeRooms[0].Amount);         // hotelList[0] luxe
        Assert.Equal(98, freeRooms[1].Amount);        // hotelList[0] default
        Assert.Equal(2, freeRooms[2].Amount);         // hotelList[0] staff

        Assert.Equal(4, freeRooms[3].Amount);         // hotelList[1] luxe
        Assert.Equal(98, freeRooms[4].Amount);        // hotelList[1] default
        Assert.Equal(2, freeRooms[5].Amount);         // hotelList[1] staff

        Assert.Equal(3, freeRooms[6].Amount);         // hotelList[2] luxe
        Assert.Equal(96, freeRooms[7].Amount);        // hotelList[2] default
        Assert.Equal(2, freeRooms[8].Amount);         // hotelList[2] staff

        Assert.Equal(5, freeRooms[9].Amount);         // hotelList[3] luxe
        Assert.Equal(100, freeRooms[10].Amount);      // hotelList[3] default
        Assert.Equal(2, freeRooms[11].Amount);        // hotelList[3] staff
    }

    /// <summary>
    /// Output information about clients who booked rooms for the highest amount of days - as unit test
    /// </summary>
    [Fact]
    public void LongestBookingClients()
    {
        // from hotel select those clients who booked room with longest booking period
        var clientsWithLongestBookingPeriod = (from hotel in _hotels
                                               from booking in hotel.Bookings
                                               orderby booking.BookingPeriodInDays descending
                                               select booking.Client).Distinct().ToList();

        Assert.Equal("Scene", clientsWithLongestBookingPeriod[0].SecondName);       // Top-1 - Charile Scene - 8 days
        Assert.Equal("Kotovich", clientsWithLongestBookingPeriod[1].SecondName);    // Top-2 - Alexey Kotovich - 7 days
        Assert.Equal("Ivanova", clientsWithLongestBookingPeriod[2].SecondName);     // Top-3 - Maria Ivanova - 6 days
        Assert.Equal("Anantha", clientsWithLongestBookingPeriod[3].SecondName);     // Top-4 - Miroslav Anantha - 5 days
        Assert.Equal("Dedova", clientsWithLongestBookingPeriod[4].SecondName);      // Top-5 - Mama Dedova - 4 days
    }

    /// <summary>
    /// Output maximum, minimum and average price of room in each hotel as unit-test
    /// </summary>
    [Fact]
    public void HotelPrices()
    {
        var prices = (from hotel in _hotels
                      select new
                      {
                          Min = hotel.Rooms.Min(r => r.Cost),
                          Max = hotel.Rooms.Max(r => r.Cost),
                          Average = hotel.Rooms.Sum(r => r.Cost) / hotel.Rooms.Count()
                      }).ToList();

        Assert.Equal((uint)10000, prices[0].Min);
        Assert.Equal((uint)35000, prices[0].Max);
        Assert.Equal(25000, prices[0].Average);

        Assert.Equal((uint)10000, prices[1].Min);
        Assert.Equal((uint)20000, prices[1].Max);
        Assert.Equal(15000, prices[1].Average);
    }
}