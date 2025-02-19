﻿using AutoMapper;
using HotelAppServer.Dto;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// <param name="logger">logger</param>
    /// <param name="context">context</param>
    /// <param name="mapper">mapper</param>
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
        _logger.LogInformation("Get all bookings");
        return await _mapper.ProjectTo<BookingGetDto>(_context.Bookings).ToListAsync();
    }

    /// <summary>
    /// Get booking by its id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Booking with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingGetDto>> GetBooking(uint id)
    {
        _logger.LogInformation("Get booking with id={Id}", id);

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            _logger.LogInformation("Can not find booking with id={Id}", id);
            return NotFound();
        }

        return _mapper.Map<BookingGetDto>(booking);
    }

    /// <summary>
    /// Updates booking in database
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="bookingToPut">booking</param>
    /// <returns>No return value or error code</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBooking(uint id, BookingPostDto bookingToPut)
    {
        _logger.LogInformation("Put booking with id={Id}", id);

        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null)
        {
            _logger.LogInformation("Can not find booking with id={Id}", id);
            return NotFound();
        };

        _mapper.Map(bookingToPut, booking);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a new booking to a database
    /// </summary>
    /// <param name="bookingToPost">booking</param>
    /// <returns>Added booking</returns>
    [HttpPost]
    public async Task<ActionResult<BookingGetDto>> PostBooking(BookingPostDto bookingToPost)
    {
        _logger.LogInformation("Post new booking");

        var mappedBooking = _mapper.Map<Booking>(bookingToPost);
        _context.Bookings.Add(mappedBooking);

        await _context.SaveChangesAsync();

        return CreatedAtAction("PostBooking", new { id = mappedBooking.Id }, _mapper.Map<BookingGetDto>(mappedBooking));
    }

    /// <summary>
    /// Deletes booking from database
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>No return value or error code</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(uint id)
    {
        _logger.LogInformation("Delete booking with id={Id}", id);

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            _logger.LogInformation("Can not find booking with id={Id}", id);
            return NotFound();
        };

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
