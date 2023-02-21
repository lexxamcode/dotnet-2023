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
    /// <summary>
    /// Passport - a string representing passport number
    /// </summary>
    public string Passport { set; get; } = String.Empty;
    /// <summary>
    /// FullName - a string for name, second_name and surname optionally
    /// </summary>
    public string FullName { set; get; } = String.Empty;
    /// <summary>
    /// BirthDate - DateTime typed value for storing birth date of person
    /// </summary>
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
