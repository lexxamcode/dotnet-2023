using HotelDomain;

namespace UnitTest;

public class HotelDomainTest
{
    [Fact]
    public void TestHotelClass()
    {
        var a = new HotelType("Bruhotel", "New-York", "Bruhstreet, 12", 12);

        var b = a.Name;

        Assert.Equal("Bruhotel", b);
    }
}