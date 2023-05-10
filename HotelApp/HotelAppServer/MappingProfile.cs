using AutoMapper;
using HotelAppServer.Dto;
using HotelDomain;
namespace HotelAppServer;

/// <summary>
/// Mapping profile for mapping Domain objects on Dto objects and vice versa
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Constructor where Maps are being created
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Client, ClientPostDto>().ReverseMap();
        CreateMap<Client, ClientGetDto>().ReverseMap();

        CreateMap<Room, RoomPostDto>().ReverseMap();
        CreateMap<Room, RoomGetDto>().ReverseMap();

        CreateMap<Booking, BookingGetDto>().ReverseMap();
        CreateMap<Booking, BookingPostDto>().ReverseMap();

        CreateMap<Hotel, HotelGetDto>().ReverseMap();
        CreateMap<Hotel, HotelPostDto>().ReverseMap();
    }
}
