using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelDomain;

public class HotelDomainContextFactory : IDesignTimeDbContextFactory<HotelDomainDbContext>
{
    public HotelDomainDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelDomainDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=HotelApp;User Id=postgres;Password=lexxamsql_p@ssw0rd;");

        return new HotelDomainDbContext(optionsBuilder.Options);
    }
}
