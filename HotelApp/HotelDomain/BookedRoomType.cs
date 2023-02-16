using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDomain;

namespace HotelDomain;
/// <summary>
/// BookedRoomType describes booked room in hotel
/// </summary>
public class BookedRoomType
{
    public RoomType Room { get; set; } = new();
    public ClientType Client { get; set; } = new();
    public DateTime CheckInDate { get; set; } = DateTime.MinValue;
    public DateTime DepartureDate { get; set; } = DateTime.MaxValue;
    public double BookingPeriodInDays 
    { 
        get => DepartureDate.Subtract(CheckInDate).Days; 

        set
        {
            if (CheckInDate.AddDays(value).Month != DepartureDate.Year  &&
                CheckInDate.AddDays(value).Month != DepartureDate.Month &&
                CheckInDate.AddDays(value).Day   != DepartureDate.Day)
                throw new ArgumentException("Departure date can not be less than check-in date");
        }
    }
    
    public BookedRoomType() { }
    public BookedRoomType(RoomType room, ClientType client, DateTime checkInDate, DateTime departureDate, double bookingPeriodInDays)
    {
        if (checkInDate > departureDate)
            throw new ArgumentException("Departure date can not be less than check-in date");

        Room = room;
        Client = client;
        CheckInDate = checkInDate;
        DepartureDate = departureDate;

        if (CheckInDate.AddDays(bookingPeriodInDays).Month != DepartureDate.Year &&
                CheckInDate.AddDays(bookingPeriodInDays).Month != DepartureDate.Month &&
                CheckInDate.AddDays(bookingPeriodInDays).Day != DepartureDate.Day)
            throw new ArgumentException("Departure date can not be less than check-in date");

        BookingPeriodInDays = bookingPeriodInDays;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BookedRoomType param)
            return false;

        return Room.Equals(param.Room) && Client.Equals(param.Client) && 
               CheckInDate == param.CheckInDate && 
               DepartureDate == param.DepartureDate &&
               BookingPeriodInDays == param.BookingPeriodInDays;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Room, Client, CheckInDate, DepartureDate, BookingPeriodInDays);
    }
}
