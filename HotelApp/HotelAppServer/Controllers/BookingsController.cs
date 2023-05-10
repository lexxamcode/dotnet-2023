using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelDomain;
using AutoMapper;
using Microsoft.Extensions.Logging;
using HotelAppServer.Dto;

namespace HotelAppServer.Controllers;

/// <summary>
/// Booking controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly HotelDomainDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor setting logger, context and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public BookingsController(ILogger<BookingsController> logger, HotelDomainDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all bookings
    /// </summary>
    /// <returns>All bookings</returns>
    [HttpGet]
    public async Task<ActionResult<List<BookingGetDto>>> GetBookings()
    {
      if (_context.Bookings == null)
      {
          return NotFound();
      }
        return await _mapper.ProjectTo<BookingGetDto>(_context.Bookings).ToListAsync();
    }

    /// <summary>
    /// Get booking by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Booking with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingGetDto>> GetBooking(uint id)
    {
      if (_context.Bookings == null)
      {
          return NotFound();
      }
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            return NotFound();
        }

        return _mapper.Map<BookingGetDto>(booking);
    }

    /// <summary>
    /// Updates booking in database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="bookingToPut"></param>
    /// <returns>No return value or error code</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBooking(uint id, BookingPostDto bookingToPut)
    {
        if (_context.Bookings == null)
            return NotFound();

        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
            return NotFound();

        _mapper.Map(bookingToPut, booking);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a new booking to a database
    /// </summary>
    /// <param name="bookingToPost"></param>
    /// <returns>Added booking</returns>
    [HttpPost]
    public async Task<ActionResult<BookingGetDto>> PostBooking(BookingPostDto bookingToPost)
    {
        if (_context.Bookings == null)
        {
            return Problem("Entity set 'HotelDomainDbContext.Bookings'  is null.");
        }

        var mappedBooking = _mapper.Map<Booking>(bookingToPost);
        _context.Bookings.Add(mappedBooking);

        await _context.SaveChangesAsync();

        return CreatedAtAction("PostBooking", new { id = mappedBooking.Id }, _mapper.Map<BookingGetDto>(mappedBooking));
    }

    /// <summary>
    /// Deletes booking from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>No return value or error code</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(uint id)
    {
        if (_context.Bookings == null)
        {
            return NotFound();
        }
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
