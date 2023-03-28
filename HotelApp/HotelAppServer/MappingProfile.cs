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
        CreateMap<Client, ClientPostDto>();
        CreateMap<ClientPostDto, Client>();

        CreateMap<Room, RoomPostDto>();
        CreateMap<RoomPostDto, Room>();
    }
}
