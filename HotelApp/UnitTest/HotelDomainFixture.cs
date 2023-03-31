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
        _clients = new()
        {
            new Client(1, "12 34 567890", "Charlie", "Scene", string.Empty, DateTime.MaxValue),
            new Client(2, "09 87 654321", "Mama", "Dedova", "Papovna", DateTime.MinValue),
            new Client(3, "10 20 304050", "Alexey", "Kotovich", "Nikolaevich", DateTime.Parse("30.01.2002")),
            new Client(4, "11 22 334455", "Maria", "Ivanova", "Ivanovna", DateTime.Parse("11.12.2003")),
            new Client(5, "66 77 889900", "Miroslav", "Anantha", string.Empty, DateTime.Parse("15.06.1991"))
        };

        _rooms = new()
        {
            // Sleepy Place rooms
            new Room(1, "Luxe", 10, 6000),
            new Room(2, "Default", 150, 2000),

            // Comfort Palace rooms
            new Room(3, "Luxe", 5, 4500),
            new Room(4, "Default", 100, 1000),

            // Chillzone Rooms
            new Room(5, "Luxe", 2, 2000),
            new Room(6, "Default", 20, 500),

            // Cheap'n'Cool Rooms
            new Room(7, "Luxe", 1, 2500),
            new Room(8, "Default", 25, 1000),

            // First Class rooms
            new Room(9, "Default", 10, 350)
        };

        _bookings = new()
        {
            // All booked rooms in test hotel "Sleepy Place" - 2 default rooms
            new Booking(1, _rooms[1], _clients[0], DateTime.Parse("9.02.2023"), 4),
            new Booking(2, _rooms[1], _clients[3], DateTime.Parse("15.07.2022"), 6),

            // All booked rooms in test hotel "Comfort Palace" - 1 luxe and 2 default rooms
            new Booking(3, _rooms[2], _clients[2], DateTime.Parse("9.02.2023"), 3),
            new Booking(4, _rooms[3], _clients[1], DateTime.Parse("23.02.2023"), 1),
            new Booking(5, _rooms[3], _clients[4], DateTime.Parse("25.5.2023"), 5),

            // All booked rooms in test Hotel "Chillzone" - 2 luxes and 4 default rooms
            new Booking(6, _rooms[4], _clients[0], DateTime.Parse("20.02.2023"), 8),
            new Booking(7,_rooms[4], _clients[1], DateTime.Parse("21.02.2023"), 4),
            new Booking(8, _rooms[5], _clients[2], DateTime.Parse("30.01.2023"), 7),
            new Booking(9, _rooms[5], _clients[1], DateTime.Parse("2.01.2023"), 4),
            new Booking(10, _rooms[5], _clients[3], DateTime.Parse("28.02.2023"), 1),
            new Booking(11, _rooms[5], _clients[4], DateTime.Parse("8.03.2023"), 2),

            // Cheap'n'Cool has no bookings

            // All booked rooms in test hotel "Firt Class" - one room
            new Booking(12, _rooms[8], _clients[0], DateTime.Parse("10.10.2022"), 10)
        };

        _hotels = new()
        {
            new Hotel(1, "Sleepy Place", "Voidburg", "Elea st. 1", new () { _rooms[0], _rooms[1] }, new() { _bookings[0], _bookings[1] }),
            new Hotel(2, "Comfort Palace", "Voidburg", "Nocturne st. 2", new() { _rooms[2], _rooms[3] }, new() { _bookings[2], _bookings[3], _bookings[4] }),
            new Hotel(3, "Chillzone", "Voidburg", "Salzburg st. 3", new() { _rooms[4], _rooms[5] }, new()
            {
                _bookings[5], _bookings[6], _bookings[7], _bookings[8], _bookings[9], _bookings[10]
            }),
            new Hotel(4, "Cheap'n'Cool", "Voidburg", "Trauma st. 4", new() { _rooms[6], _rooms[7] }, new()),
            new Hotel(5, "First Class", "Nullvillage", "Toi st. 5", new() { _rooms[8] }, new() {_bookings[11]})
        };
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
