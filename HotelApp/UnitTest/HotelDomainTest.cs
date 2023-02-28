using HotelDomain;

namespace UnitTest;

public class HotelDomainTest
{
    // Initializes standart list of rooms for tests
    private List<Room> CreateDefaultRooms()
    {
        return new List<Room>()
        {
            new Room(Guid.NewGuid(), "Luxe", 5, 4500),
            new Room(Guid.NewGuid(), "Default", 100, 1000),
            new Room(Guid.NewGuid(), "Staff", 2, 0)
        };
    }
    // Initializes standart list of clients for tests
    private List<Client> CreateClientsList()
    {
        // Test values representing clients
        return new List<Client>()
        {
            new Client(Guid.NewGuid(), "12 34 567890", "Charlie", "Scene", string.Empty, DateTime.MaxValue),
            new Client(Guid.NewGuid(), "09 87 654321", "Mama", "Dedova", "Papovna", DateTime.MinValue),
            new Client(Guid.NewGuid(), "10 20 304050", "Alexey", "Kotovich", "Nikolaevich", DateTime.Parse("30.01.2002")),
            new Client(Guid.NewGuid(), "11 22 334455", "Maria", "Ivanova", "Ivanovna", DateTime.Parse("11.12.2003")),
            new Client(Guid.NewGuid(), "66 77 889900", "Miroslav", "Anantha", string.Empty, DateTime.Parse("15.06.1991"))
        };
    }
    // Initializes 5 hotels for tests
    private List<Hotel> CreateFilledHotelList()
    {
        // List of test hotels
        var hotelList = new List<Hotel>
        {
            new Hotel(Guid.NewGuid(), "Sleepy Place", "Voidburg", "Elea st. 1"),
            new Hotel(Guid.NewGuid(), "Comfort Palace", "Voidburg", "Nocturne st. 2"),
            new Hotel(Guid.NewGuid(), "Chillzone", "Voidburg", "Salzburg st. 3"),
            new Hotel(Guid.NewGuid(), "Cheap'n'Cool", "Voidburg", "Trauma st. 4"),
            new Hotel(Guid.NewGuid(), "First Class", "Nullvillage", "Toi st. 5")
        };

        // Test values representing 3 types of room available in each hotel;
        var roomList = CreateDefaultRooms();
        var clientList = CreateClientsList();

        // All booked rooms in test Hotel "Chillzone" - 2 luxes and 4 default rooms
        var chillzoneBookedRoomsList = new List<BookedRoom>
        {
            new BookedRoom(Guid.NewGuid(), roomList[0], clientList[0], DateTime.Parse("20.02.2023"), 8),
            new BookedRoom(Guid.NewGuid(), roomList[0], clientList[1], DateTime.Parse("21.02.2023"), 4),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[2], DateTime.Parse("30.01.2023"), 7),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[1], DateTime.Parse("2.01.2023"), 4),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[3], DateTime.Parse("28.02.2023"), 1),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[4], DateTime.Parse("8.03.2023"), 2)
        };
        hotelList[2].BookedRooms = chillzoneBookedRoomsList;

        // All booked rooms in test hotel "Comfort Palace" - 1 luxe and 2 default rooms
        var comfortPalaceBookedRoomsList = new List<BookedRoom>
        {
            new BookedRoom(Guid.NewGuid(), roomList[0], clientList[2], DateTime.Parse("9.02.2023"), 3),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[1], DateTime.Parse("23.02.2023"), 1),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[4], DateTime.Parse("25.5.2023"), 5),
        };
        hotelList[1].BookedRooms = comfortPalaceBookedRoomsList;

        // All booked rooms in test hotel "Sleepy Place" - 2 default rooms
        var sleepyPlaceBookedRoomsList = new List<BookedRoom>
        {
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[0], DateTime.Parse("9.02.2023"), 4),
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[3], DateTime.Parse("15.07.2022"), 6),
        };
        hotelList[0].BookedRooms = sleepyPlaceBookedRoomsList;

        // All booked rooms in test hotel "Firt Class" - one room
        var firstClassBookedRoomsList = new List<BookedRoom>
        {
            new BookedRoom(Guid.NewGuid(), roomList[1], clientList[0], DateTime.Parse("10.10.2022"), 10)
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
        var defaultRoom = new Room();
        // Full constructor of RoomType
        var initializedRoom = new Room(Guid.NewGuid(), "luxe", 5, 4500);

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
        var defaultClient = new Client();
        // Full constructor of ClientType
        var initializedClient = new Client(Guid.NewGuid(), "12 34 567890", "Claudia", "Frigg", string.Empty, DateTime.MaxValue);

        Assert.Equal(string.Empty, defaultClient.Passport);
        Assert.Equal(string.Empty, defaultClient.FirstName);
        Assert.Equal(DateTime.MinValue, defaultClient.BirthDate);

        Assert.Equal("12 34 567890", initializedClient.Passport);
        Assert.Equal("Claudia", initializedClient.FirstName);
        Assert.Equal(DateTime.MaxValue, initializedClient.BirthDate);
    }

    [Fact]
    public void BookedRoomConstructorsTest()
    {
        // Default constructor of BookedRoomType
        var defaultBookedRoom = new BookedRoom();

        // Full constructor of BookedRoomType
        var testRoom = new Room(Guid.NewGuid(), "default", 1, 1);
        var testClient = new Client(Guid.NewGuid(), "00 00 000000", "Name", "SecondName", string.Empty, DateTime.MaxValue);

        var initializedBookedRoom = new BookedRoom(Guid.NewGuid(), testRoom, testClient, DateTime.MinValue, 3);

        Assert.Equal(new Room(), defaultBookedRoom.Room);
        Assert.Equal(new Client(), defaultBookedRoom.Client);
        Assert.Equal(DateTime.MinValue, defaultBookedRoom.CheckInDate);
        Assert.Equal(DateTime.MinValue, defaultBookedRoom.DepartureDate);

        Assert.Equal(testRoom, initializedBookedRoom.Room);
        Assert.Equal(testClient, initializedBookedRoom.Client);
        Assert.Equal(DateTime.MinValue, initializedBookedRoom.CheckInDate);
        Assert.Equal(DateTime.MinValue.AddDays(3), initializedBookedRoom.DepartureDate);
        Assert.Equal((uint)initializedBookedRoom.DepartureDate.Subtract(initializedBookedRoom.CheckInDate).Days, initializedBookedRoom.BookingPeriodInDays);
    }

    [Fact]
    public void HotelConstructorsTest()
    {
        // Test values for RoomType type
        var luxeRoom = new Room(Guid.NewGuid(), "Luxe", 5, 4500);
        var defaultRoom = new Room(Guid.NewGuid(), "Default", 100, 1000);
        var staffRoom = new Room(Guid.NewGuid(), "Staff", 2, 0);

        // Test values for ClientType type
        var firstClient = new Client(Guid.NewGuid(), "12 34 567890", "Charlie", "Scene", string.Empty, DateTime.MaxValue);
        var secondClient = new Client(Guid.NewGuid(), "09 87 654321", "Dedova", "Mama", "Papovna", DateTime.MinValue);

        // Test values for BookedRoomType type
        var bookedLuxe = new BookedRoom(Guid.NewGuid(), luxeRoom, firstClient, new DateTime(2023, 2, 16), 4);
        var bookedDefaultRoom = new BookedRoom(Guid.NewGuid(), luxeRoom, secondClient, new DateTime(2023, 2, 11), 1);

        // Test containers of RoomType, BookedRoomType and ClientType types
        var listOfRooms = new List<Room>();
        var listOfBookedRooms = new List<BookedRoom>();
        var listOfClients = new List<Client>();

        // Filling test values into collections
        listOfRooms.Add(luxeRoom);
        listOfRooms.Add(defaultRoom);
        listOfRooms.Add(staffRoom);

        listOfBookedRooms.Add(bookedDefaultRoom);
        listOfBookedRooms.Add(bookedLuxe);

        listOfClients.Add(firstClient);
        listOfClients.Add(secondClient);

        // Default constructor for Hotel
        var emptyHotel = new Hotel();
        // Constructor without collections for Hotel
        var knownHotel = new Hotel(Guid.NewGuid(), "Known Hotel", "Samara", "Moscow Highway, 34");
        // Constructor with full bunch of parameters
        var wellKnownHotel = new Hotel(Guid.NewGuid(), "Well Known Hotel", "Moscow", "Lenina st, 13a", listOfRooms, listOfBookedRooms, listOfClients);

        Assert.Equal(string.Empty, emptyHotel.Name);

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
        var requestHotelList = (from hotel in CreateFilledHotelList()
                               select hotel).ToList();

        Assert.Equal("Sleepy Place", requestHotelList[0].Name);
        Assert.Equal("Voidburg", requestHotelList[1].City);
        Assert.Equal("Salzburg st. 3", requestHotelList[2].Address);
        Assert.NotEmpty(requestHotelList[3].Clients);
        Assert.NotEmpty(requestHotelList[4].Rooms);
        Assert.NotEmpty(requestHotelList[4].Clients);
    }

    [Fact]
    // Output all information about hotel clients as unit test
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

    [Fact]
    //Output information about clients who booked rooms for the highest amount of days - as unit test
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

    [Fact]
    // Output maximum, minimum and average price of room in each hotel as unit-test
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

        var secondHotel = new Hotel(Guid.NewGuid(),"test hotel 2", "test city", "test address");
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