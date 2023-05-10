using AutoMapper;
using HotelAppServer.Dto;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAppServer.Controllers;

/// <summary>
/// Hotel controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly ILogger<HotelsController> _logger;
    private readonly HotelDomainDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// HotelsController constructor
    /// </summary>
    /// <param name="logger">logger</param>
    /// <param name="context">context</param>
    /// <param name="mapper">mapper</param>
    public HotelsController(ILogger<HotelsController> logger, HotelDomainDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all hotels
    /// </summary>
    /// <returns>All hotels</returns>
    [HttpGet]
    public async Task<ActionResult<List<HotelGetDto>>> GetHotels()
    {
        if (_context.Hotels == null)
        {
            return NotFound();
        }
        return await _mapper.ProjectTo<HotelGetDto>(_context.Hotels).ToListAsync();
    }

    /// <summary>
    /// Get hotel by its id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Hotel with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelGetDto>> GetHotel(uint id)
    {
        if (_context.Hotels == null)
        {
            return NotFound();
        }
        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return _mapper.Map<HotelGetDto>(hotel);
    }

    /// <summary>
    /// Updates existing hotel in database
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="hotelToPut">hotel</param>
    /// <returns>No return value or error code</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(uint id, HotelPostDto hotelToPut)
    {
        if (_context.Hotels == null)
            return NotFound();

        var hotel = await _context.Hotels.FindAsync(id);

        if (hotel == null)
            return NotFound();

        _mapper.Map(hotelToPut, hotel);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a new hotel to the database
    /// </summary>
    /// <param name="hotelToPost">hotel</param>
    /// <returns>New hotel instance</returns>
    [HttpPost]
    public async Task<ActionResult<HotelGetDto>> PostHotel(HotelPostDto hotelToPost)
    {
        if (_context.Hotels == null)
        {
            return Problem("Entity set 'HotelDomainDbContext.Hotels'  is null.");
        }

        var mappedHotel = _mapper.Map<Hotel>(hotelToPost);
        _context.Hotels.Add(mappedHotel);

        await _context.SaveChangesAsync();

        return CreatedAtAction("PostHotel", new { id = mappedHotel.Id }, _mapper.Map<HotelGetDto>(mappedHotel));
    }

    /// <summary>
    /// Deletes hotel from database
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>No return value or error code</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(uint id)
    {
        if (_context.Hotels == null)
        {
            return NotFound();
        }
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
