using HotelDomain;

namespace UnitTest;

public class HotelDomainTest
{
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
        var hotelList = new List<HotelType>
        {
            new HotelType("Sleepy Place", "Voidburg", "Elea st. 1"),
            new HotelType("Comfort Palace", "Voidburg", "Nocturne st. 2"),
            new HotelType("Chillzone", "Voidburg", "Salzburg st. 3"),
            new HotelType("Cheap'n'cool", "Voidburg", "Trauma st. 4"),
            new HotelType("First class", "Nullvillage", "Toi st. 5")
        };

        var roomList = new List<RoomType>
        {
            new RoomType("Luxe", 5, 4500),
            new RoomType("Default", 100, 1000),
            new RoomType("Staff", 2, 0)
        };

        var clientList = new List<ClientType>
        {
            new ClientType("12 34 567890", "Charlie Scene", DateTime.MaxValue),
            new ClientType("09 87 654321", "Dedova Mama Papovna", DateTime.MinValue)
        };

        hotelList[4].Rooms = roomList;
        hotelList[4].Clients = clientList;

        Assert.Equal("Sleepy Place", hotelList[0].Name);
        Assert.Equal("Voidburg", hotelList[1].City);
        Assert.Equal("Salzburg st. 3", hotelList[2].Address);
        Assert.Empty(hotelList[3].Clients);
        Assert.NotEmpty(hotelList[4].Rooms);
        Assert.NotEmpty(hotelList[4].Clients);
    }

    [Fact]
    // Output all information about hotel clients as unit test
    public void SecondRequestTest()
    {
        var clientList = new List<ClientType>
        {
            new ClientType("12 34 567890", "Charlie Scene", DateTime.MaxValue),
            new ClientType("09 87 654321", "Dedova Mama Papovna", DateTime.MinValue)
        };

        var hotel = new HotelType("Sleepy Place", "Voidburg", "Elea st. 1");
        hotel.Clients = clientList;

        Assert.Equal("Charlie Scene", hotel.Clients[0].FullName);
        Assert.Equal("09 87 654321", hotel.Clients[1].Passport);
        Assert.Equal(DateTime.MinValue, hotel.Clients[1].BirthDate);
    }
}