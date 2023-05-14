using HotelDomain;

namespace UnitTest;

public class HotelDomainTest : IClassFixture<HotelDomainFixture>
{
    private readonly HotelDomainFixture _hotelFixture;

    public HotelDomainTest(HotelDomainFixture hotelFixture)
    {
        _hotelFixture = hotelFixture;
    }

    /// <summary>
    /// Output information about all hotels in database as unit test
    /// </summary>
    [Fact]
    public void AllHotelsTest()
    {
        var requestHotelList = (from hotel in _hotelFixture.Hotels
                                select hotel).ToList();

        Assert.Equal("Sleepy Place", requestHotelList[0].Name);
        Assert.Equal("Voidburg", requestHotelList[1].City);
        Assert.Equal("Salzburg st. 3", requestHotelList[2].Address);
        Assert.Equal("Default", requestHotelList[3].Rooms[1].Type);
        Assert.Equal(2, requestHotelList[3].Rooms.Count);
        Assert.NotEmpty(requestHotelList[4].Rooms);
    }

    /// <summary>
    /// Output all information about hotel clients as unit test
    /// </summary>
    [Fact]
    public void HotelClientsTest()
    {
        // Sleepy Place has 2 clients - Charlie Scene and Maria Ivanova
        var clients = (from hotel in _hotelFixture.Hotels
                       where hotel.Name == "Sleepy Place"
                       join room in _hotelFixture.Rooms on hotel.Id equals room.HotelId
                       from booking in room.Bookings
                       join client in _hotelFixture.Clients on booking.ClientId equals client.Id
                       orderby client.LastName
                       select client).ToList();

        Assert.Equal("Ivanova", clients[0].LastName);
        Assert.Equal("Scene", clients[1].LastName);
    }

    /// <summary>
    /// Top-5 most booked hotels as unit test
    /// </summary>
    [Fact]
    public void TopHotels()
    {
        // Sort hotelList by booked rooms count using LINQ
        var linqSortedHotelList = (from room in _hotelFixture.Rooms
                                   join hotel in _hotelFixture.Hotels on room.HotelId equals hotel.Id
                                   group room by room.HotelId into hotelRooms
                                   select new
                                   {
                                       HotelName = (from hotel in _hotelFixture.Hotels where hotel.Id == hotelRooms.Key.Value select hotel.Name).Single(),
                                       Bookings = hotelRooms.Sum(e => e.Bookings.Count)
                                   }).OrderByDescending(hotelRooms => hotelRooms.Bookings).ToList();

        foreach(var hotel in linqSortedHotelList)
        {
            Console.WriteLine(hotel.HotelName);
            Console.WriteLine(hotel.Bookings);
        }

        Assert.Equal("Chillzone", linqSortedHotelList[0].HotelName);       // "Chillzone" has 6 booked rooms
        Assert.Equal("Comfort Palace", linqSortedHotelList[1].HotelName);  // "Comfort" Palace has 3 booked rooms
        Assert.Equal("Sleepy Place", linqSortedHotelList[2].HotelName);    // "Sleepy" Place has 2 booked rooms
        Assert.Equal("First Class", linqSortedHotelList[3].HotelName);     // "First" class has 1 booked room
        Assert.Equal("Cheap'n'Cool", linqSortedHotelList[4].HotelName);    // "Cheap'n'cool" has no booked rooms
    }

    /// <summary>
    /// Output information about all available rooms at all hotels in one city as unit test
    /// "Sleepy Place"       hotelList[0] has 2 booked default rooms                        => available: 10 luxe, 148 default
    /// "Comfort Palace"     hotelList[1] has 3 booked rooms: 1 luxe and 2 default rooms    => available: 4 luxe, 98 default
    /// "Chillzone"          hotelList[2] has 6 booked rooms: 2 luxes and 4 default rooms   => available: 0 luxe, 16 default
    /// "Cheap'n'Cool"       hotelList[3] has no booked rooms                               => available: 1 luxe, 25 default
    /// "First Class"        hotelList[4] has 1 booked default room                         => available: 0 luxe, 9 default 
    /// </summary>
    [Fact]
    public void AvailableRooms()
    {
        var freeRooms = (from hotel in _hotelFixture.Hotels
                         where hotel.City == "Voidburg"
                         from room in hotel.Rooms!
                         select new
                         {
                             Hotel = hotel.Name,
                             Type = room.Type,
                             Amount = room.Amount - (from bookedRoom in room.Bookings
                                                     where bookedRoom.RoomId.Equals(room.Id)
                                                     select bookedRoom).Count()
                         }).ToList();

        Assert.Equal(10, freeRooms[0].Amount);         // hotelList[0] luxe
        Assert.Equal(148, freeRooms[1].Amount);        // hotelList[0] default

        Assert.Equal(4, freeRooms[2].Amount);         // hotelList[1] luxe
        Assert.Equal(98, freeRooms[3].Amount);        // hotelList[1] default

        Assert.Equal(0, freeRooms[4].Amount);         // hotelList[2] luxe
        Assert.Equal(16, freeRooms[5].Amount);        // hotelList[2] default

        Assert.Equal(1, freeRooms[6].Amount);         // hotelList[3] luxe
        Assert.Equal(25, freeRooms[7].Amount);         // hotelList[3] default
    }

    /// <summary>
    /// Output information about clients who booked rooms for the highest amount of days - as unit test
    /// </summary>
    [Fact]
    public void LongestBookingClients()
    {
        // from hotel select those clients who booked room with longest booking period
        var clientsWithLongestBookingPeriod = (from booking in _hotelFixture.Bookings
                                               join client in _hotelFixture.Clients on booking.ClientId equals client.Id
                                               orderby booking.BookingPeriodInDays descending
                                               select client).Distinct().ToList();

        Assert.Equal("Scene", clientsWithLongestBookingPeriod[0].LastName);       // Top-1 - Charile Scene - 8 days
        Assert.Equal("Kotovich", clientsWithLongestBookingPeriod[1].LastName);    // Top-2 - Alexey Kotovich - 7 days
        Assert.Equal("Ivanova", clientsWithLongestBookingPeriod[2].LastName);     // Top-3 - Maria Ivanova - 6 days
        Assert.Equal("Anantha", clientsWithLongestBookingPeriod[3].LastName);     // Top-4 - Miroslav Anantha - 5 days
        Assert.Equal("Dedova", clientsWithLongestBookingPeriod[4].LastName);      // Top-5 - Mama Dedova - 4 days
    }

    /// <summary>
    /// Output maximum, minimum and average price of room in each hotel as unit-test
    /// </summary>
    [Theory]
    [InlineData("Sleepy Place", 6000, 2000, 4000)]
    [InlineData("Comfort Palace", 4500, 1000, 2750)]
    [InlineData("Chillzone", 2000, 500, 1250)]
    [InlineData("Cheap'n'Cool", 2500, 1000, 1750)]
    [InlineData("First Class", 350, 350, 350)]
    public void HotelPrices(string hotelName, uint max, uint min, uint average)
    {
        var prices = (from hotel in _hotelFixture.Hotels
                      where hotel.Name == hotelName
                      select new
                      {
                          Min = hotel.Rooms.Min(r => r.Cost),
                          Max = hotel.Rooms.Max(r => r.Cost),
                          Average = hotel.Rooms.Sum(r => r.Cost) / hotel.Rooms.Count()
                      }).ToList();

        Assert.Equal(min, prices[0].Min);
        Assert.Equal(max, prices[0].Max);
        Assert.Equal(average, prices[0].Average);
    }
}