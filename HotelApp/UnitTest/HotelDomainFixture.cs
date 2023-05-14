using HotelDomain;

namespace UnitTest;

public class HotelDomainFixture
{
    private readonly List<Client> _clients;
    private readonly List<Room> _rooms;
    private readonly List<Booking> _bookings;
    private readonly List<Hotel> _hotels;

    public HotelDomainFixture()
    {
        var clients = new List<Client>()
        {
            new Client() { Id = 1,    Passport = "12 34 567890",    FirstName = "Charlie",  LastName = "Scene",     Surname = string.Empty,   BirthDate = DateTime.MaxValue },
            new Client() { Id = 2,    Passport = "09 87 654321",    FirstName = "Mama",     LastName = "Dedova",    Surname = "Papovna",      BirthDate = DateTime.MinValue },
            new Client() { Id = 3,    Passport = "10 20 304050",    FirstName = "Alexey",   LastName = "Kotovich",  Surname = "Nikolaevich",  BirthDate = DateTime.Parse("30.01.2002") },
            new Client() { Id = 4,    Passport = "11 22 334455",    FirstName = "Maria",    LastName = "Ivanova",   Surname = "Ivanovna",     BirthDate = DateTime.Parse("11.12.2003") },
            new Client() { Id = 5,    Passport = "66 77 889900",    FirstName = "Miroslav", LastName = "Anantha",   Surname = string.Empty,   BirthDate = DateTime.Parse("15.06.1991") }
        };

        var hotels = new List<Hotel>
        {
            new Hotel() { Id = 1, Name = "Sleepy Place",    City = "Voidburg",      Address = "Elea st. 1" },
            new Hotel() { Id = 2, Name = "Comfort Palace",  City = "Voidburg",      Address="Nocturne st. 2" },
            new Hotel() { Id = 3, Name = "Chillzone",       City = "Voidburg",      Address = "Salzburg st. 3" },
            new Hotel() { Id = 4, Name = "Cheap'n'Cool",    City = "Voidburg",      Address = "Trauma st. 4" },
            new Hotel() { Id = 5, Name = "First Class",     City = "Nullvillage",   Address = "Toi st. 5" }
        };

        var rooms = new List<Room>()
        {
            // Sleepy Place rooms
            new Room() { Id=1,  HotelId=1,  Type = "Luxe",          Amount = 10,    Cost=6000 },
            new Room() { Id=2,  HotelId=1,  Type = "Default",       Amount = 150,   Cost = 2000 },

            // Comfort Palace rooms
            new Room() { Id = 3,  HotelId = 2,  Type = "Luxe",      Amount = 5,     Cost=4500 },
            new Room() { Id = 4,  HotelId = 2,  Type = "Default",   Amount = 100,   Cost = 1000 },

            // Chillzone Rooms
            new Room() { Id = 5, HotelId = 3, Type = "Luxe",        Amount = 2,     Cost = 2000 },
            new Room() { Id = 6, HotelId = 3, Type = "Default",     Amount = 20,    Cost = 500 },

            // Cheap'n'Cool Rooms
            new Room() { Id = 7, HotelId = 4, Type = "Luxe",        Amount = 1,     Cost = 2500 },
            new Room() { Id = 8, HotelId = 4, Type = "Default",     Amount = 25,    Cost = 1000 },

            // First Class rooms
            new Room() { Id = 9, HotelId = 5, Type = "Default",     Amount = 10,    Cost = 350 }
        };

        var bookings = new List<Booking>()
        {
            // All booked rooms in test hotel "Sleepy Place" - 2 default rooms
            new Booking() { Id = 1,     RoomId = 2, ClientId = 1, CheckInDate = DateTime.Parse("9.02.2023"),    BookingPeriodInDays = 4 },
            new Booking() { Id = 2,     RoomId = 2, ClientId = 4, CheckInDate = DateTime.Parse("15.07.2022"),   BookingPeriodInDays = 6 },

            // All booked rooms in test hotel "Comfort Palace" - 1 luxe and 2 default rooms
            new Booking() { Id = 3,     RoomId = 3, ClientId = 3, CheckInDate = DateTime.Parse("9.02.2023"),    BookingPeriodInDays = 3 },
            new Booking() { Id = 4,     RoomId = 4, ClientId = 2, CheckInDate = DateTime.Parse("23.02.2023"),   BookingPeriodInDays = 1 },
            new Booking() { Id = 5,     RoomId = 4, ClientId = 5, CheckInDate = DateTime.Parse("25.5.2023"),    BookingPeriodInDays = 5 },

            // All booked rooms in test Hotel "Chillzone" - 2 luxes and 4 default rooms
            new Booking() { Id = 6,     RoomId = 5, ClientId = 1, CheckInDate = DateTime.Parse("20.02.2023"),   BookingPeriodInDays = 8 },
            new Booking() { Id = 7,     RoomId = 5, ClientId = 2, CheckInDate = DateTime.Parse("21.02.2023"),   BookingPeriodInDays = 4 },
            new Booking() { Id = 8,     RoomId = 6, ClientId = 3, CheckInDate = DateTime.Parse("30.01.2023"),   BookingPeriodInDays = 7 },
            new Booking() { Id = 9,     RoomId = 6, ClientId = 2, CheckInDate = DateTime.Parse("2.01.2023"),    BookingPeriodInDays = 4 },
            new Booking() { Id = 10,    RoomId = 6, ClientId = 4, CheckInDate = DateTime.Parse("28.02.2023"),   BookingPeriodInDays = 1 },
            new Booking() { Id = 11,    RoomId = 6, ClientId = 5, CheckInDate = DateTime.Parse("8.03.2023"),    BookingPeriodInDays = 2 },

            // Cheap'n'Cool has no bookings

            // All booked rooms in test hotel "Firt Class" - one room
            new Booking() { Id = 12,    RoomId = 9, ClientId = 1, CheckInDate = DateTime.Parse("10.10.2022"),   BookingPeriodInDays = 10 }
        };

        clients[0].Bookings = new List<Booking>() { bookings[0], bookings[5], bookings[11] };
        clients[1].Bookings = new List<Booking>() { bookings[3], bookings[6], bookings[8] };
        clients[2].Bookings = new List<Booking>() { bookings[2], bookings[7] };
        clients[3].Bookings = new List<Booking>() { bookings[1], bookings[9] };
        clients[4].Bookings = new List<Booking>() { bookings[4], bookings[10] };

        rooms[0].Bookings = new List<Booking>();
        rooms[1].Bookings = new List<Booking>() { bookings[0], bookings[1] };
        rooms[2].Bookings = new List<Booking>() { bookings[2] };
        rooms[3].Bookings = new List<Booking>() { bookings[3], bookings[4] };
        rooms[4].Bookings = new List<Booking>() { bookings[5], bookings[6] };
        rooms[5].Bookings = new List<Booking>() { bookings[7], bookings[8], bookings[9], bookings[10] };
        rooms[6].Bookings = new List<Booking>();
        rooms[7].Bookings = new List<Booking>();
        rooms[8].Bookings = new List<Booking>() { bookings[11] };

        hotels[0].Rooms = new List<Room>() { rooms[0], rooms[1] };
        hotels[1].Rooms = new List<Room>() { rooms[2], rooms[3] };
        hotels[2].Rooms = new List<Room>() { rooms[4], rooms[5] };
        hotels[3].Rooms = new List<Room>() { rooms[6], rooms[7] };
        hotels[4].Rooms = new List<Room>() { rooms[8] };

        _clients = clients;
        _hotels = hotels;
        _rooms = rooms;
        _bookings = bookings;
    }

    /// <summary>
    /// Test values of Client class
    /// </summary>
    /// <returns>List of test clients</returns>
    public List<Client> Clients => _clients;

    /// <summary>
    /// Initializes rooms for each hotel
    /// [0-1] - Sleepy Place
    /// [2-3] - Comfort Palace
    /// [4-5] - Chillzone
    /// [6-7] - Cheap'n'Cool
    /// [8] - First Class
    /// </summary>
    /// <returns>Return a list of lists of rooms of each hotel. Size = 9</returns>
    public List<Room> Rooms => _rooms;

    /// <summary>
    /// Initializes all bookings for test
    /// [0-1]   - Sleepy Place
    /// [2-4]   - Comfort Palace
    /// [5-10]  - Chillzone
    /// Cheap'n'Cool has no bookings
    /// [11]    - First Class
    /// </summary>
    /// <returns>List of list of Bookings for each hotel. Size = 12</returns>
    public List<Booking> Bookings => _bookings;
    /// <summary>
    /// Test values of Hotel class
    /// </summary>
    /// <returns>List of test hotels</returns>
    public List<Hotel> Hotels => _hotels;
}
