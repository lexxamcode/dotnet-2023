using Microsoft.EntityFrameworkCore;

namespace HotelDomain;
/// <summary>
/// Hotel database context
/// </summary>
public class HotelDomainDbContext : DbContext
{
    /// <summary>
    /// Clients DbSet
    /// </summary>
    public DbSet<Client> Clients { get; set; }
    /// <summary>
    /// Hotels DbSet
    /// </summary>
    public DbSet<Hotel> Hotels { get; set; }
    /// <summary>
    /// Rooms DbSet
    /// </summary>
    public DbSet<Room> Rooms { get; set; }
    /// <summary>
    /// Bookings DbSet
    /// </summary>
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// DbContext constructor
    /// </summary>
    /// <param name="options">options</param>
    public HotelDomainDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    /// <summary>
    /// Fill Database on creating
    /// </summary>
    /// <param name="modelBuilder">modelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        clients[0].Bookings?.Add(bookings[0]);
        clients[0].Bookings?.Add(bookings[5]);
        clients[0].Bookings?.Add(bookings[11]);
        clients[1].Bookings?.Add(bookings[3]);
        clients[1].Bookings?.Add(bookings[6]);
        clients[1].Bookings?.Add(bookings[8]);
        clients[2].Bookings?.Add(bookings[2]);
        clients[2].Bookings?.Add(bookings[7]);
        clients[3].Bookings?.Add(bookings[1]);
        clients[3].Bookings?.Add(bookings[9]);
        clients[4].Bookings?.Add(bookings[4]);
        clients[4].Bookings?.Add(bookings[10]);

        rooms[1].Bookings?.Add(bookings[0]);
        rooms[1].Bookings?.Add(bookings[1]);
        rooms[2].Bookings?.Add(bookings[2]);
        rooms[3].Bookings?.Add(bookings[3]);
        rooms[3].Bookings?.Add(bookings[4]);
        rooms[4].Bookings?.Add(bookings[5]);
        rooms[4].Bookings?.Add(bookings[6]);
        rooms[5].Bookings?.Add(bookings[7]);
        rooms[5].Bookings?.Add(bookings[8]);
        rooms[5].Bookings?.Add(bookings[9]);
        rooms[5].Bookings?.Add(bookings[10]);
        rooms[7].Bookings?.Add(bookings[11]);

        hotels[0].Rooms?.Add(rooms[0]);
        hotels[0].Rooms?.Add(rooms[1]);
        hotels[1].Rooms?.Add(rooms[2]);
        hotels[1].Rooms?.Add(rooms[3]);
        hotels[2].Rooms?.Add(rooms[4]);
        hotels[2].Rooms?.Add(rooms[5]);
        hotels[3].Rooms?.Add(rooms[6]);
        hotels[3].Rooms?.Add(rooms[7]);
        hotels[4].Rooms?.Add(rooms[8]);

        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Hotel>().HasData(hotels);
        modelBuilder.Entity<Room>().HasData(rooms);
        modelBuilder.Entity<Booking>().HasData(bookings);
    }
}
