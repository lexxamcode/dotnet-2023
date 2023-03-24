using HotelDomain;

namespace UnitTest;

internal static class TestDataRepository
{
    /// <summary>
    /// Initializes test values of Client class
    /// </summary>
    /// <returns>List of test clients</returns>
    public static List<Client> GetClientList()
    {
        return new()
        {
            new Client(1, "12 34 567890", "Charlie", "Scene", string.Empty, DateTime.MaxValue),
            new Client(2, "09 87 654321", "Mama", "Dedova", "Papovna", DateTime.MinValue),
            new Client(3, "10 20 304050", "Alexey", "Kotovich", "Nikolaevich", DateTime.Parse("30.01.2002")),
            new Client(4, "11 22 334455", "Maria", "Ivanova", "Ivanovna", DateTime.Parse("11.12.2003")),
            new Client(5, "66 77 889900", "Miroslav", "Anantha", string.Empty, DateTime.Parse("15.06.1991"))
        };
    }
    /// <summary>
    /// Initializes rooms for each hotel
    /// [0] - Sleepy Place
    /// [1] - Comfort Palace
    /// [2] - Chillzone
    /// [3] - Cheap'n'Cool
    /// [4] - First Class
    /// </summary>
    /// <returns>Return a list of lists of rooms of each hotel. Size = 5</returns>
    public static List<List<Room>> GetAllRoomsList()
    {
        return new()
        {
            // Sleepy Place rooms
            new()
            {
                new Room(1, "Luxe", 10, 6000),
                new Room(2, "Default", 150, 2000)
            },
            // Comfort Palace rooms
            new()
            {
                new Room(3, "Luxe", 5, 4500),
                new Room(4, "Default", 100, 1000)
            },
            // Chillzone Rooms
            new()
            {
                new Room(5, "Luxe", 2, 2000),
                new Room(6, "Default", 20, 500)
            },
            // Cheap'n'Cool Rooms
            new()
            {
                new Room(7, "Luxe", 1, 2500),
                new Room(8, "Default", 25, 1000)
            },
            // First Class rooms
            new()
            {
                new Room(9, "Default", 10, 350)
            }
        };
    }
    /// <summary>
    /// Initializes all bookings for test
    /// [0] - Sleepy Place
    /// [1] - Comfort Palace
    /// [2] - Chillzone
    /// [3] - Cheap'n'Cool
    /// [4] - First Class
    /// </summary>
    /// <returns>List of list of Bookings for each hotel. Size = 5</returns>
    public static List<List<Booking>> GetAllBookingsList()
    {
        var clientList = GetClientList();
        var roomList = GetAllRoomsList();
        return new()
        {
            // All booked rooms in test hotel "Sleepy Place" - 2 default rooms
            new()
            {
                new Booking(1, roomList[0][1], clientList[0], DateTime.Parse("9.02.2023"), 4),
                new Booking(2, roomList[0][1], clientList[3], DateTime.Parse("15.07.2022"), 6),
            },
            // All booked rooms in test hotel "Comfort Palace" - 1 luxe and 2 default rooms
            new()
            {
                new Booking(3, roomList[1][0], clientList[2], DateTime.Parse("9.02.2023"), 3),
                new Booking(4, roomList[1][1], clientList[1], DateTime.Parse("23.02.2023"), 1),
                new Booking(5, roomList[1][1], clientList[4], DateTime.Parse("25.5.2023"), 5),
            },
            // All booked rooms in test Hotel "Chillzone" - 2 luxes and 4 default rooms
            new()
            {
                new Booking(6, roomList[2][0], clientList[0], DateTime.Parse("20.02.2023"), 8),
                new Booking(7, roomList[2][0], clientList[1], DateTime.Parse("21.02.2023"), 4),
                new Booking(8, roomList[2][1], clientList[2], DateTime.Parse("30.01.2023"), 7),
                new Booking(9, roomList[2][1], clientList[1], DateTime.Parse("2.01.2023"), 4),
                new Booking(10, roomList[2][1], clientList[3], DateTime.Parse("28.02.2023"), 1),
                new Booking(11, roomList[2][1], clientList[4], DateTime.Parse("8.03.2023"), 2)
            },

            // Cheap'n'Cool has no bookings

            // All booked rooms in test hotel "Firt Class" - one room
            new()
            {
                new Booking(12, roomList[4][0], clientList[0], DateTime.Parse("10.10.2022"), 10)
            }
        };
    }
    public static List<Hotel> GetHotelList()
    {
        var roomList = GetAllRoomsList();
        var bookingsList = GetAllBookingsList();
        var hotelList = new List<Hotel>
        {
            new Hotel(1, "Sleepy Place", "Voidburg", "Elea st. 1", roomList[0], bookingsList[0]),
            new Hotel(2, "Comfort Palace", "Voidburg", "Nocturne st. 2", roomList[1], bookingsList[1]),
            new Hotel(3, "Chillzone", "Voidburg", "Salzburg st. 3", roomList[2], bookingsList[2]),
            new Hotel(4, "Cheap'n'Cool", "Voidburg", "Trauma st. 4", roomList[3], new()),
            new Hotel(5, "First Class", "Nullvillage", "Toi st. 5", roomList[4], bookingsList[3])
        };

        return hotelList;
    }
}
