using HotelDomain;
using System.Security.Cryptography;
using Xunit.Sdk;

namespace UnitTest;

public class HotelDomainTest
{
    // Initializes standart list of rooms for tests
    private List<RoomType> CreateDefaultRooms()
    {
        return new List<RoomType>()
        {
            new RoomType("Luxe", 5, 4500),
            new RoomType("Default", 100, 1000),
            new RoomType("Staff", 2, 0)
        };
    }
    // Initializes standart list of clients for tests
    private List<ClientType> CreateClientsList()
    {
        // Test values representing clients
        return new List<ClientType>()
        {
            new ClientType("12 34 567890", "Charlie Scene", DateTime.MaxValue),
            new ClientType("09 87 654321", "Dedova Mama Papovna", DateTime.MinValue),
            new ClientType("10 20 304050", "Kotovich Alexey Nikolaevich", DateTime.Parse("30.01.2002")),
            new ClientType("11 22 334455", "Ivanova Maria Ivanovna", DateTime.Parse("11.12.2003")),
            new ClientType("66 77 889900", "Miroslav Anantha", DateTime.Parse("15.06.1991"))
        };
    }
    // Initializes 5 hotels for tests
    private List<HotelType> CreateFilledHotelList()
    {
        // List of test hotels
        var hotelList = new List<HotelType>
        {
            new HotelType("Sleepy Place", "Voidburg", "Elea st. 1"),
            new HotelType("Comfort Palace", "Voidburg", "Nocturne st. 2"),
            new HotelType("Chillzone", "Voidburg", "Salzburg st. 3"),
            new HotelType("Cheap'n'Cool", "Voidburg", "Trauma st. 4"),
            new HotelType("First Class", "Nullvillage", "Toi st. 5")
        };

        // Test values representing 3 types of room available in each hotel;
        var roomList = CreateDefaultRooms();
        var clientList = CreateClientsList();

        // All booked rooms in test Hotel "Chillzone" - 2 luxes and 4 default rooms
        var chillzoneBookedRoomsList = new List<BookedRoomType>
        {
            new BookedRoomType(roomList[0], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[0], clientList[1], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days)
        };
        hotelList[2].BookedRooms = chillzoneBookedRoomsList;

        // All booked rooms in test hotel "Comfort Palace" - 1 luxe and 2 default rooms
        var comfortPalaceBookedRoomsList = new List<BookedRoomType>
        {
            new BookedRoomType(roomList[0], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[1], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
        };
        hotelList[1].BookedRooms = comfortPalaceBookedRoomsList;

        // All booked rooms in test hotel "Sleepy Place" - 2 default rooms
        var sleepyPlaceBookedRoomsList = new List<BookedRoomType>
        {
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
            new BookedRoomType(roomList[1], clientList[1], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
        };
        hotelList[0].BookedRooms = sleepyPlaceBookedRoomsList;

        // All booked rooms in test hotel "Firt Class" - one room
        var firstClassBookedRoomsList = new List<BookedRoomType>
        {
            new BookedRoomType(roomList[1], clientList[0], DateTime.MinValue, DateTime.MaxValue, DateTime.MaxValue.Subtract(DateTime.MinValue).Days),
        };
        hotelList[4].BookedRooms = firstClassBookedRoomsList;

        foreach (var hotel in hotelList)
        {
            hotel.Rooms = roomList;
            hotel.Clients = clientList;
        }

        return hotelList;
    }

    [Fact]
    public void RoomConstructorsTest()
    {
        // Default constructor of RoomType
        var defaultRoom = new RoomType();
        // Full constructor of RoomType
        var initializedRoom = new RoomType("luxe", 5, 4500);

        Assert.Equal(string.Empty, defaultRoom.Type);
        Assert.Equal(uint.MinValue, defaultRoom.Amount);
        Assert.Equal(uint.MinValue, defaultRoom.Cost);

        uint amount = 5;
        uint cost = 4500;

        Assert.Equal("luxe", initializedRoom.Type);
        Assert.Equal(amount, initializedRoom.Amount);
        Assert.Equal(cost, initializedRoom.Cost);
    }

    [Fact]
    public void ClientConstructorsTest()
    {
        // Default constructor of ClientType
        var defaultClient = new ClientType();
        // Full constructor of ClientType
        var initializedClient = new ClientType("12 34 567890", "Claudia Frigg", DateTime.MaxValue);

        Assert.Equal(string.Empty, defaultClient.Passport);
        Assert.Equal(string.Empty, defaultClient.FullName);
        Assert.Equal(DateTime.MinValue, defaultClient.BirthDate);

        Assert.Equal("12 34 567890", initializedClient.Passport);
        Assert.Equal("Claudia Frigg", initializedClient.FullName);
        Assert.Equal(DateTime.MaxValue, initializedClient.BirthDate);
    }

    [Fact]
    public void BookedRoomConstructorsTest()
    {
        // Default constructor of BookedRoomType
        var defaultBookedRoom = new BookedRoomType();

        // Full constructor of BookedRoomType
        var testRoom = new RoomType("default", 1, 1);
        var testClient = new ClientType("00 00 000000", "Name SecondName", DateTime.MaxValue);

        var initializedBookedRoom = new BookedRoomType(testRoom, testClient, DateTime.MinValue, DateTime.MinValue.AddDays(3), 3);

        Assert.Equal(new RoomType(), defaultBookedRoom.Room);
        Assert.Equal(new ClientType(), defaultBookedRoom.Client);
        Assert.Equal(DateTime.MinValue, defaultBookedRoom.CheckInDate);
        Assert.Equal(DateTime.MaxValue, defaultBookedRoom.DepartureDate);
        Assert.Equal(DateTime.MaxValue.Subtract(DateTime.MinValue).Days, defaultBookedRoom.BookingPeriodInDays);

        Assert.Equal(testRoom, initializedBookedRoom.Room);
        Assert.Equal(testClient, initializedBookedRoom.Client);
        Assert.Equal(DateTime.MinValue, initializedBookedRoom.CheckInDate);
        Assert.Equal(DateTime.MinValue.AddDays(3), initializedBookedRoom.DepartureDate);
        Assert.Equal(initializedBookedRoom.DepartureDate.Subtract(initializedBookedRoom.CheckInDate).Days, initializedBookedRoom.BookingPeriodInDays);
    }

    [Fact]
    public void HotelConstructorsTest()
    {
        // Test values for RoomType type
        var luxeRoom = new RoomType("Luxe", 5, 4500);
        var defaultRoom = new RoomType("Default", 100, 1000);
        var staffRoom = new RoomType("Staff", 2, 0);

        // Test values for ClientType type
        var firstClient = new ClientType("12 34 567890", "Charlie Scene", DateTime.MaxValue);
        var secondClient = new ClientType("09 87 654321", "Dedova Mama Papovna", DateTime.MinValue);

        // Test values for BookedRoomType type
        var bookedLuxe = new BookedRoomType(luxeRoom, firstClient, new DateTime(2023, 2, 16), new DateTime(2023, 2, 20), 4);
        var bookedDefaultRoom = new BookedRoomType(luxeRoom, secondClient, new DateTime(2023, 2, 11), new DateTime(2023, 2, 12), 1);

        // Test containers of RoomType, BookedRoomType and ClientType types
        var listOfRooms = new List<RoomType>();
        var listOfBookedRooms = new List<BookedRoomType>();
        var listOfClients = new List<ClientType>();

        // Filling test values into collections
        listOfRooms.Add(luxeRoom);
        listOfRooms.Add(defaultRoom);
        listOfRooms.Add(staffRoom);

        listOfBookedRooms.Add(bookedDefaultRoom);
        listOfBookedRooms.Add(bookedLuxe);

        listOfClients.Add(firstClient);
        listOfClients.Add(secondClient);

        // Default constructor for HotelType
        var emptyHotel = new HotelType();
        // Constructor without collections for HotelType
        var knownHotel = new HotelType("Known Hotel", "Samara", "Moscow Highway, 34");
        // Constructor with full bunch of parameters
        var wellKnownHotel = new HotelType("Well Known Hotel", "Moscow", "Lenina st, 13a", listOfRooms, listOfBookedRooms, listOfClients);

        Assert.Equal(String.Empty, emptyHotel.Name);

        Assert.Equal("Samara", knownHotel.City);
        Assert.Equal("Moscow Highway, 34", knownHotel.Address);

        Assert.Equal(2, wellKnownHotel.BookedRooms.Count);
        Assert.Equal(luxeRoom, wellKnownHotel.Rooms[0]);
        Assert.Equal(secondClient, wellKnownHotel.Clients[1]);
    }

    [Fact]
    // Output information about all hotels in database as unit test
    public void FirstRequestTest()
    {
        var requestHotelList = from hotel in CreateFilledHotelList()
                               select hotel;

        Assert.Equal("Sleepy Place", requestHotelList.ElementAt(0).Name);
        Assert.Equal("Voidburg", requestHotelList.ElementAt(1).City);
        Assert.Equal("Salzburg st. 3", requestHotelList.ElementAt(2).Address);
        Assert.NotEmpty(requestHotelList.ElementAt(3).Clients);
        Assert.NotEmpty(requestHotelList.ElementAt(4).Rooms);
        Assert.NotEmpty(requestHotelList.ElementAt(4).Clients);
    }

    [Fact]
    // Output all information about hotel clients as unit test
    public void SecondRequestTest()
    {
        var hotelList = CreateFilledHotelList();

        var clients = from hotel in hotelList where hotel.Name == "Sleepy Place"
                      from client in hotel.Clients
                      orderby client.FullName ascending
                      select client;

        Assert.Equal("Charlie Scene", clients.ElementAt(0).FullName);
        Assert.Equal("09 87 654321", clients.ElementAt(1).Passport);
        Assert.Equal(DateTime.MinValue, clients.ElementAt(1).BirthDate);
    }

    [Fact]
    // Top-5 most booked hotels as unit test
    public void ThirdRequestTest()
    {
        var hotelList = CreateFilledHotelList();

        // Sort hotelList by booked rooms count using LINQ
        var linqSortedHotelList = from hotel in hotelList
                                  orderby hotel.BookedRooms.Count descending
                                  select hotel;

        Assert.Equal("Chillzone", linqSortedHotelList.ElementAt(0).Name);       // "Chillzone" has 6 booked rooms
        Assert.Equal("Comfort Palace", linqSortedHotelList.ElementAt(1).Name);  // "Comfort" Palace has 3 booked rooms
        Assert.Equal("Sleepy Place", linqSortedHotelList.ElementAt(2).Name);    // "Sleepy" Place has 2 booked rooms
        Assert.Equal("First Class", linqSortedHotelList.ElementAt(3).Name);     // "First" class has 1 booked room
        Assert.Equal("Cheap'n'Cool", linqSortedHotelList.ElementAt(4).Name);    // "Cheap'n'cool" has no booked rooms
    }

    [Fact]
    // Output information about all available rooms at all hotels in one city as unit test
    public void FourthRequestTest()
    {
        // Each hotel in this test has 5 luxe rooms and 100 default rooms
        //
        // "Sleepy Place"       hotelList[0] has 2 booked default rooms                         => available: 5 luxe, 98 default
        // "Comfort Palace"     hotelList[1] has 3 booked rooms: 1 luxe and 2 default rooms     => available: 4 luxe, 98 default
        // "Chillzone"          hotelList[2] has 6 booked rooms: 2 luxes and 4 default rooms    => available: 3 luxe, 96 default
        // "Cheap'n'Cool"       hotelList[3] has no booked rooms                                => available: 5 luxe, 100 default
        // "First Class"        hotelList[4] has 1 booked default room                          => available: 5 luxe, 99 default

        var freeRooms = from hotel in CreateFilledHotelList()
                        where hotel.City == "Voidburg"
                        from room in hotel.Rooms
                        select new
                        {
                            Hotel = hotel.Name,
                            Type = room.Type,
                            Amount = room.Amount - (from booked_room in hotel.BookedRooms
                                                    where booked_room.Room.Equals(room)
                                                    select booked_room).Count()
                        };

        Assert.Equal((uint)5, freeRooms.ElementAt(0).Amount);   // hotelList[0] luxe
        Assert.Equal((uint)98, freeRooms.ElementAt(1).Amount);  // hotelList[0] default
        Assert.Equal((uint)2, freeRooms.ElementAt(2).Amount);   // hotelList[0] staff

        Assert.Equal((uint)4, freeRooms.ElementAt(3).Amount);   // hotelList[1] luxe
        Assert.Equal((uint)98, freeRooms.ElementAt(4).Amount);  // hotelList[1] default
        Assert.Equal((uint)2, freeRooms.ElementAt(5).Amount);   // hotelList[1] staff

        Assert.Equal((uint)3, freeRooms.ElementAt(6).Amount);   // hotelList[2] luxe
        Assert.Equal((uint)96, freeRooms.ElementAt(7).Amount);  // hotelList[2] default
        Assert.Equal((uint)2, freeRooms.ElementAt(8).Amount);   // hotelList[2] staff

        Assert.Equal((uint)5, freeRooms.ElementAt(9).Amount);      // hotelList[3] luxe
        Assert.Equal((uint)100, freeRooms.ElementAt(10).Amount);   // hotelList[3] default
        Assert.Equal((uint)2, freeRooms.ElementAt(11).Amount);     // hotelList[3] staff
    }

    [Fact]
    //Output information about clients who booked rooms for the highest amount of days - as unit test
    public void FifthRequestTest()
    {
        var hotel = new HotelType("test hotel", "test city", "test address");
        hotel.Clients = CreateClientsList();
        hotel.Rooms = CreateDefaultRooms();

        hotel.BookedRooms = new List<BookedRoomType>()
        {
            // Charlie Scene - 2 days
            new BookedRoomType(hotel.Rooms[0], hotel.Clients[0],
                               DateTime.Parse("14.02.2023"), DateTime.Parse("16.02.2023"),
                               DateTime.Parse("16.02.2023").Subtract(DateTime.Parse("14.02.2023")).TotalDays),
            // Dedova Mama Papovna - 6 days
            new BookedRoomType(hotel.Rooms[1], hotel.Clients[1],
                               DateTime.Parse("15.02.2023"), DateTime.Parse("21.02.2023"),
                               DateTime.Parse("21.02.2023").Subtract(DateTime.Parse("15.02.2023")).TotalDays),
            // Kotovich Alexey Nikolaevich - 1 day
            new BookedRoomType(hotel.Rooms[1], hotel.Clients[2],
                               DateTime.Parse("16.02.2023"), DateTime.Parse("17.02.2023"),
                               DateTime.Parse("17.02.2023").Subtract(DateTime.Parse("18.02.2023")).TotalDays),
            // Ivanova Maria Ivanovna - 5 days
            new BookedRoomType(hotel.Rooms[1], hotel.Clients[3],
                               DateTime.Parse("16.02.2023"), DateTime.Parse("21.02.2023"),
                               DateTime.Parse("21.02.2023").Subtract(DateTime.Parse("18.02.2023")).TotalDays),
            // Miroslav Anantha - 3 days
            new BookedRoomType(hotel.Rooms[1], hotel.Clients[4],
                               DateTime.Parse("21.02.2023"), DateTime.Parse("24.02.2023"),
                               DateTime.Parse("24.02.2023").Subtract(DateTime.Parse("21.02.2023")).TotalDays)

        };

        // from hotel select those clients who booked room with longest booking period
        var clientsWithLongestBookingPeriod = from room in hotel.BookedRooms
                                 orderby room.BookingPeriodInDays descending
                                 select room.Client;

        Assert.Equal("Dedova Mama Papovna", clientsWithLongestBookingPeriod.ElementAt(0).FullName);
        Assert.Equal("Ivanova Maria Ivanovna", clientsWithLongestBookingPeriod.ElementAt(1).FullName);
        Assert.Equal("Miroslav Anantha", clientsWithLongestBookingPeriod.ElementAt(2).FullName);
        Assert.Equal("Charlie Scene", clientsWithLongestBookingPeriod.ElementAt(3).FullName);
        Assert.Equal("Kotovich Alexey Nikolaevich", clientsWithLongestBookingPeriod.ElementAt(4).FullName);
    }

    [Fact]
    // Output maximum, minimum and average price of room in each hotel as unit-test
    public void SixthRequestTest()
    {
        var firstHotel = new HotelType("test hotel 1", "test city", "test address");
        firstHotel.Rooms = new List<RoomType>()
        {
            new RoomType("Luxe", 5, 35000),
            new RoomType("High-class", 30, 30000),
            new RoomType("Default", 100, 10000)
        };
        // average cost = (35+30+10)/3 = 25 0000 

        var secondHotel = new HotelType("test hotel 2", "test city", "test address");
        secondHotel.Rooms = new List<RoomType>()
        {
            new RoomType("Luxe", 10, 20000),
            new RoomType("High-class", 20, 15000),
            new RoomType("Default", 40, 10000)
        };
        // average cost = (20+15+10)/3 = 15 0000

        var hotelList = new List<HotelType> { firstHotel, secondHotel };

        var prices = from hotel in hotelList
                     select new
                     {
                         Min = hotel.Rooms.Min(r => r.Cost),
                         Max = hotel.Rooms.Max(r => r.Cost),
                         Average = hotel.Rooms.Sum(r => r.Cost) / hotel.Rooms.Count()
                     };

        Assert.Equal((uint)10000, prices.ElementAt(0).Min);
        Assert.Equal((uint)35000, prices.ElementAt(0).Max);
        Assert.Equal((uint)25000, prices.ElementAt(0).Average);

        Assert.Equal((uint)10000, prices.ElementAt(1).Min);
        Assert.Equal((uint)20000, prices.ElementAt(1).Max);
        Assert.Equal((uint)15000, prices.ElementAt(1).Average);
    }
}