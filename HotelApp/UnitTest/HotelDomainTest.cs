using HotelDomain;

namespace UnitTest;

public class HotelDomainTest
{
    /// <summary>
    /// Output information about all hotels in database as unit test
    /// </summary>
    [Fact]
    public void FirstRequestTest()
    {
        var requestHotelList = (from hotel in CreateFilledHotelList()
                                select hotel).ToList();

        Assert.Equal("Sleepy Place", requestHotelList[0].Name);
        Assert.Equal("Voidburg", requestHotelList[1].City);
        Assert.Equal("Salzburg st. 3", requestHotelList[2].Address);
        Assert.NotEmpty(requestHotelList[3].Clients);
        Assert.NotEmpty(requestHotelList[4].Rooms);
        Assert.NotEmpty(requestHotelList[4].Clients);
    }

    /// <summary>
    /// Output all information about hotel clients as unit test
    /// </summary>
    [Fact]
    public void SecondRequestTest()
    {
        var hotelList = CreateFilledHotelList();

        var clients = (from hotel in hotelList
                       where hotel.Name == "Sleepy Place"
                       from client in hotel.Clients
                       orderby client.SecondName
                       select client).ToList();

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
    public void ThirdRequestTest()
    {
        var hotelList = CreateFilledHotelList();

        // Sort hotelList by booked rooms count using LINQ
        var linqSortedHotelList = (from hotel in hotelList
                                   orderby hotel.BookedRooms.Count descending
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
    public void FourthRequestTest()
    {
        var freeRooms = (from hotel in CreateFilledHotelList()
                         where hotel.City == "Voidburg"
                         from room in hotel.Rooms
                         select new
                         {
                             Hotel = hotel.Name,
                             Type = room.Type,
                             Amount = room.Amount - (from bookedRoom in hotel.BookedRooms
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
    public void FifthRequestTest()
    {
        var hotel = new Hotel(Guid.NewGuid(), "test hotel", "test city", "test address");
        hotel.Clients = CreateClientsList();
        hotel.Rooms = CreateDefaultRooms();

        hotel.BookedRooms = new List<BookedRoom>()
        {
            // Charlie Scene - 2 days
            new BookedRoom(Guid.NewGuid(), hotel.Rooms[0], hotel.Clients[0], DateTime.Parse("14.02.2023"), 2),
            // Dedova Mama Papovna - 6 days
            new BookedRoom(Guid.NewGuid(), hotel.Rooms[1], hotel.Clients[1], DateTime.Parse("15.02.2023"), 6),
            // Kotovich Alexey Nikolaevich - 1 day
            new BookedRoom(Guid.NewGuid(), hotel.Rooms[1], hotel.Clients[2], DateTime.Parse("16.02.2023"), 1),
            // Ivanova Maria Ivanovna - 5 days
            new BookedRoom(Guid.NewGuid(), hotel.Rooms[1], hotel.Clients[3], DateTime.Parse("16.02.2023"), 5),
            // Miroslav Anantha - 3 days
            new BookedRoom(Guid.NewGuid(), hotel.Rooms[1], hotel.Clients[4], DateTime.Parse("21.02.2023"), 3)

        };

        // from hotel select those clients who booked room with longest booking period
        var clientsWithLongestBookingPeriod = (from room in hotel.BookedRooms
                                               orderby room.BookingPeriodInDays descending
                                               select room.Client).ToList();

        Assert.Equal("Dedova", clientsWithLongestBookingPeriod[0].SecondName);
        Assert.Equal("Ivanova", clientsWithLongestBookingPeriod[1].SecondName);
        Assert.Equal("Anantha", clientsWithLongestBookingPeriod[2].SecondName);
        Assert.Equal("Scene", clientsWithLongestBookingPeriod[3].SecondName);
        Assert.Equal("Kotovich", clientsWithLongestBookingPeriod[4].SecondName);
    }

    /// <summary>
    /// Output maximum, minimum and average price of room in each hotel as unit-test
    /// </summary>
    [Fact]
    public void SixthRequestTest()
    {
        var firstHotel = new Hotel(Guid.NewGuid(), "test hotel 1", "test city", "test address");
        firstHotel.Rooms = new List<Room>()
        {
            new Room(Guid.NewGuid(), "Luxe", 5, 35000),
            new Room(Guid.NewGuid(), "High-class", 30, 30000),
            new Room(Guid.NewGuid(), "Default", 100, 10000)
        };
        // average cost = (35+30+10)/3 = 25 0000 

        var secondHotel = new Hotel(Guid.NewGuid(), "test hotel 2", "test city", "test address");
        secondHotel.Rooms = new List<Room>()
        {
            new Room(Guid.NewGuid(), "Luxe", 10, 20000),
            new Room(Guid.NewGuid(), "High-class", 20, 15000),
            new Room(Guid.NewGuid(), "Default", 40, 10000)
        };
        // average cost = (20+15+10)/3 = 15 0000

        var hotelList = new List<Hotel> { firstHotel, secondHotel };

        var prices = (from hotel in hotelList
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