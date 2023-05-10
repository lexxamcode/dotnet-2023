using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HotelAppServer.Dto;
using HotelDomain;
using Microsoft.EntityFrameworkCore;

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
    private readonly HotelDomainDbContext _dbContextFactory;

    /// <summary>
    /// Constructor setting logger, data repository and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="dbContextFactory"></param>
    public QueryController(ILogger<QueryController> logger, IMapper mapper, HotelDomainDbContext dbContextFactory)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Returns information about all hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet("all_hotels")]
    public async Task<ActionResult<HotelGetDto>> GetAllHotels()
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();

        _logger.LogInformation("GET information about all hotels");

        var request = await (from hotel in _dbContextFactory.Hotels
                             select _mapper.Map<HotelGetDto>(hotel)).ToListAsync();
        return Ok(request);
    }

    /// <summary>
    /// Returns list of clients of hotel with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of clients of hotel with given id</returns>
    [HttpGet("hotel_clients/{id}")]
    public async Task<ActionResult<List<ClientGetDto>>> GetClients(int id)
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();

        _logger.LogInformation("GET clients of hotel with id = {Id}", id);
        var hotelCheckExisting = await _dbContextFactory.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id);

        if (hotelCheckExisting == null)
        {
            _logger.LogInformation("Not found hotel with id = {Id}", id);
            return NotFound();
        }

        return await (from hotel in _dbContextFactory.Hotels
                      where hotel.Id == id
                        join room in _dbContextFactory.Rooms on hotel.Id equals room.HotelId
                join booking in _dbContextFactory.Bookings on room.Id equals booking.RoomId
                join client in _dbContextFactory.Clients on booking.ClientId equals client.Id
                orderby client.LastName
                select _mapper.Map<ClientGetDto>(client)).Distinct().ToListAsync();
    }

    /// <summary>
    /// Returns 5 most booked hotels
    /// </summary>
    /// <returns>5 most booked hotels</returns>
    [HttpGet("top5")]
    public async Task<ActionResult<dynamic>> GetTopFiveHotels()
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();
        _logger.LogInformation("GET top 5 most booked hotels");

        var result = await (from room in _dbContextFactory.Rooms
                            join hotel in _dbContextFactory.Hotels on room.HotelId equals hotel.Id
                            group room by room.HotelId into hotelRooms
                            select new
                            {
                                HotelName = (from hotel in _dbContextFactory.Hotels where hotel.Id == hotelRooms.Key.Value select hotel.Name).Single(),
                                Bookings = hotelRooms.Sum(e => e.Bookings.Count)
                            }).OrderByDescending(hotelRooms => hotelRooms.Bookings).ToListAsync();
        return Ok(result);
    }

    /// <summary>
    /// Returns list of available rooms in given city
    /// </summary>
    /// <param name="city"></param>
    /// <returns>list of available rooms in given city</returns>
    [HttpGet("available_rooms/{city}")]
    public async Task<ActionResult<dynamic>> GetAvailableRoomsInCity(string city)
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();

        _logger.LogInformation("GET available rooms in {City}", city);

        var hotelCheckExisting = _dbContextFactory.Hotels.FirstOrDefault(hotel => hotel.City == city);

        if (hotelCheckExisting == null)
        {
            _logger.LogInformation("Not found Hotels in city {City}", city);
            return NotFound();
        }

        var result = await (from hotel in _dbContextFactory.Hotels
                            where hotel.City == city
                            from room in hotel.Rooms!
                            select new
                            {
                                Hotel = hotel.Name,
                                Type = room.Type,
                                Amount = room.Amount - (from bookedRoom in room.Bookings
                                                       where bookedRoom.RoomId.Equals(room.Id)
                                                       select bookedRoom).Count()
                            }).ToListAsync();

        return result;
    }


    /// <summary>
    /// Returns list of clients who booked room with longest booking period
    /// </summary>
    /// <returns>list of clients who booked room with longest booking period</returns>
    [HttpGet("clients_with_longest_bookings")]
    public async Task<List<ClientGetDto>> GetClientsWithLongestBookings()
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();
        _logger.LogInformation("GET clients with longest booking period");

        var result = await (from booking in _dbContextFactory.Bookings
                            join client in _dbContextFactory.Clients on booking.ClientId equals client.Id
                            orderby booking.BookingPeriodInDays descending
                            select _mapper.Map<ClientGetDto>(client)).Distinct().ToListAsync();

        return result;
    }

    /// <summary>
    /// Returns minimum, maximum and average room price for each hotel
    /// </summary>
    /// <returns>minimum, maximum and average room price for each hotel</returns>
    [HttpGet("prices")]
    public async Task<dynamic> GetPrices()
    {
        //await using var context = await _dbContextFactory.CreateDbContextAsync();

        _logger.LogInformation("GET prices for each hotel");

        var result = await (from hotel in _dbContextFactory.Hotels
                            where hotel.Rooms.Count() != 0
                            select new
                            {
                                HotelName = hotel.Name,
                                Min = hotel.Rooms!.Min(r => r.Cost),
                                Max = hotel.Rooms!.Max(r => r.Cost),
                                Average = hotel.Rooms.Sum(r => r.Cost) / hotel.Rooms.Count()
                            }).ToListAsync();

        return result;
    }
}
