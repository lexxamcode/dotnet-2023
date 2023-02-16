using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
/// <summary>
/// ClientType describes a person registered at hotel
/// </summary>
public class ClientType
{
    public string Passport { set; get; } = String.Empty;
    public string FullName { set; get; } = String.Empty;
    public DateTime BirthDate { set; get; } = DateTime.MinValue;

    public ClientType() { }
    public ClientType(string passport, string fullName, DateTime birthDate)
    {
        Passport = passport;
        FullName = fullName;
        BirthDate = birthDate;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ClientType param)
            return false;

        return Passport == param.Passport && FullName == param.FullName && BirthDate == param.BirthDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Passport, FullName, BirthDate);
    }
}
