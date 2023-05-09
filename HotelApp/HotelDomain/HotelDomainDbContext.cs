using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDomain;
public class HotelDomainDbContext: DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
       
    public HotelDomainDbContext(DbContextOptions options): base(options)
    {
        Database.EnsureCreated();
    }
}
