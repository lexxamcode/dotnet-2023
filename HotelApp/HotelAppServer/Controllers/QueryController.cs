using AutoMapper;
using HotelAppServer.Dto;
using HotelAppServer.Repository;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelAppServer.Controllers;

/// <summary>
/// Query controller for processing queries
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class QueryController : ControllerBase
{
    private readonly ILogger<QueryController> _logger;
    private readonly IMapper _mapper;
    private readonly IHotelAppRepository _hotelAppRepository;

    /// <summary>
    /// Constructor setting logger, data repository and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="hotelAppRepository"></param>
    public QueryController(ILogger<QueryController> logger, IMapper mapper, IHotelAppRepository hotelAppRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _hotelAppRepository = hotelAppRepository;
    }

    /// <summary>
    /// Returns information about all hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet("all_hotels")]
    public List<HotelGetDto> GetAllHotels()
    {
        _logger.LogInformation("GET information about all hotels");
        return (from hotel in _hotelAppRepository.Hotels
                select _mapper.Map<HotelGetDto>(hotel)).ToList();
    }

    /// <summary>
    /// Returns list of clients of hotel with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of clients of hotel with given id</returns>
    [HttpGet("hotel_clients/{id}")]
    public ActionResult<List<Client>> GetClients(int id)
    {
        _logger.LogInformation("GET clients of hotel with id = {Id}", id);
        var hotelCheckExisting = _hotelAppRepository.Hotels.FirstOrDefault(hotel => hotel.Id == id);

        if (hotelCheckExisting == null)
        {
            _logger.LogInformation("Not found hotel with id = {Id}", id);
            return NotFound();
        }

        return (from hotel in _hotelAppRepository.Hotels
                where hotel.Id == id
                from booking in hotel.Bookings
                orderby booking.Client.LastName
                select booking.Client).ToList();
    }

    /// <summary>
    /// Returns 5 most booked hotels
    /// </summary>
    /// <returns>5 most booked hotels</returns>
    [HttpGet("top5")]
    public List<HotelGetDto> GetTopFiveHotels()
    {
        _logger.LogInformation("GET top 5 most booked hotels");

        return (from hotel in _hotelAppRepository.Hotels
                orderby hotel.Bookings.Count descending
                select _mapper.Map<HotelGetDto>(hotel)).Take(5).ToList();
    }

    /// <summary>
    /// Returns list of available rooms in given city
    /// </summary>
    /// <param name="city"></param>
    /// <returns>list of available rooms in given city</returns>
    [HttpGet("available_rooms/{city}")]
    public ActionResult<dynamic> GetAvailableRoomsInCity(string city)
    {
        _logger.LogInformation("GET available rooms in {City}", city);

        var hotelCheckExisting = _hotelAppRepository.Hotels.FirstOrDefault(hotel => hotel.City == city);

        if (hotelCheckExisting == null)
        {
            _logger.LogInformation("Not found Hotels in city {City}", city);
            return NotFound();
        }

        return (from hotel in _hotelAppRepository.Hotels
                where hotel.City == city
                from room in hotel.Rooms
                select new
                {
                    Hotel = hotel.Name,
                    Type = room.Type,
                    Amount = room.Amount - (from bookedRoom in hotel.Bookings
                                            where bookedRoom.Room.Equals(room)
                                            select bookedRoom).Count()
                }).ToList();
    }


    /// <summary>
    /// Returns list of clients who booked room with longest booking period
    /// </summary>
    /// <returns>list of clients who booked room with longest booking period</returns>
    [HttpGet("clients_with_longest_bookings")]
    public List<Client> GetClientsWithLongestBookings()
    {
        _logger.LogInformation("GET clients with longest booking period");

        return (from hotel in _hotelAppRepository.Hotels
                from booking in hotel.Bookings
                orderby booking.BookingPeriodInDays descending
                select booking.Client).Distinct().ToList();
    }

    /// <summary>
    /// Returns minimum, maximum and average room price for each hotel
    /// </summary>
    /// <returns>minimum, maximum and average room price for each hotel</returns>
    [HttpGet("prices")]
    public dynamic GetPrices()
    {
        _logger.LogInformation("GET prices for each hotel");
        return (from hotel in _hotelAppRepository.Hotels
                select new
                {
                    HotelName = hotel.Name,
                    Min = hotel.Rooms.Min(r => r.Cost),
                    Max = hotel.Rooms.Max(r => r.Cost),
                    Average = hotel.Rooms.Sum(r => r.Cost) / hotel.Rooms.Count()
                }).ToList();
    }
}
