using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
public class ClientType
{
    public string Passport { set; get; }
    public string FullName { set; get; }
    public DateTime BirthDate { set; get; }

    public ClientType(string passport, string fullName, DateTime birthDate)
    {
        Passport = passport;
        FullName = fullName;
        BirthDate = birthDate;
    }
}
