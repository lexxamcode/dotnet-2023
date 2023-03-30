using AutoMapper;
using HotelAppServer.Dto;
using HotelAppServer.Repository;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelAppServer.Controllers;

/// <summary>
/// Booking controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IMapper _mapper;
    private readonly IHotelAppRepository _hotelAppRepository;

    /// <summary>
    /// Constructor setting logger, mapper and data repository
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="hotelAppRepository"></param>
    public BookingController(ILogger<BookingController> logger, IMapper mapper, IHotelAppRepository hotelAppRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _hotelAppRepository = hotelAppRepository;
    }

    /// <summary>
    /// Returns list of all bookings
    /// </summary>
    /// <returns>list of all bookings</returns>
    [HttpGet]
    public List<BookingGetDto> Get()
    {
        _logger.LogInformation("GET list of all bookings");
        return _hotelAppRepository.Bookings.Select(booking => _mapper.Map<BookingGetDto>(booking)).ToList();
    }

    /// <summary>
    /// Return booking with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>booking with given id</returns>
    [HttpGet("{id}")]
    public ActionResult<BookingGetDto> Get(int id)
    {
        _logger.LogInformation("GET booking with id={Id}", id);
        var booking = _hotelAppRepository.Bookings.FirstOrDefault(booking => booking.Id == id);

        if (booking == null)
        {
            _logger.LogInformation("Not found booking with id = {Id}", id);
            return NotFound();
        }

        return Ok(_mapper.Map<BookingGetDto>(booking));
    }

    /// <summary>
    /// Adds booking to repository
    /// </summary>
    /// <param name="bookingToPost"></param>
    [HttpPost]
    public void Post([FromBody] BookingPostDto bookingToPost)
    {
        _logger.LogInformation("POST booking: {roomId}, {clientID}, {checkInDate}, {bookingPeriodInDays}",
                               bookingToPost.RoomId,
                               bookingToPost.ClientId,
                               bookingToPost.CheckInDate,
                               bookingToPost.BookingPeriodInDays);
        _hotelAppRepository.Bookings.Add(_mapper.Map<Booking>(bookingToPost));
    }

    /// <summary>
    /// Updates booking in repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="bookingToPut"></param>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] BookingPostDto bookingToPut)
    {
        _logger.LogInformation("POST booking with id = {Id}: {roomId}, {clientID}, {checkInDate}, {bookingPeriodInDays}",
                               id,
                               bookingToPut.RoomId,
                               bookingToPut.ClientId,
                               bookingToPut.CheckInDate,
                               bookingToPut.BookingPeriodInDays);

        var booking = _hotelAppRepository.Bookings.FirstOrDefault(booking => booking.Id == id);

        if (booking == null)
        {
            _logger.LogInformation("Not found booking with id = {Id}", id);
            return NotFound();
        }

        booking = _mapper.Map(bookingToPut, booking);
        return Ok();

    }

    /// <summary>
    /// Deletes booking from repository by its id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("DELETE booking with id = {Id}", id);
        var booking = _hotelAppRepository.Bookings.FirstOrDefault(booking => booking.Id == id);

        if (booking == null)
        {
            _logger.LogInformation("Not found booking with id = {Id}", id);
            return NotFound();
        }

        _hotelAppRepository.Bookings.Remove(booking);
        return Ok();
    }
}
