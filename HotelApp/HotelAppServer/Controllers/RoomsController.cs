using AutoMapper;
using HotelAppServer.Dto;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAppServer.Controllers;

/// <summary>
/// Room Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly ILogger<RoomsController> _logger;
    private readonly HotelDomainDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor setting logger, context and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public RoomsController(ILogger<RoomsController> logger, HotelDomainDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all rooms
    /// </summary>
    /// <returns>All rooms</returns>
    [HttpGet]
    public async Task<ActionResult<List<RoomGetDto>>> GetRooms()
    {
        if (_context.Rooms == null)
        {
            return NotFound();
        }
        return await _mapper.ProjectTo<RoomGetDto>(_context.Rooms).ToListAsync();
    }

    /// <summary>
    /// Gets room by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Room with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomGetDto>> GetRoom(uint id)
    {
        if (_context.Rooms == null)
        {
            return NotFound();
        }
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        return _mapper.Map<RoomGetDto>(room);
    }

    /// <summary>
    /// Updates existing room in database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="roomToPut"></param>
    /// <returns>No return value or error code</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(uint id, RoomPostDto roomToPut)
    {
        if (_context.Rooms == null)
            return NotFound();

        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
            return NotFound();

        _mapper.Map(roomToPut, room);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a new room to the database
    /// </summary>
    /// <param name="room"></param>
    /// <returns>New room instance</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Room>> PostRoom(RoomPostDto room)
    {
        if (_context.Rooms == null)
        {
            return Problem("Entity set 'HotelDomainDbContext.Rooms'  is null.");
        }

        var mappedRoom = _mapper.Map<Room>(room);
        _context.Rooms.Add(mappedRoom);
        await _context.SaveChangesAsync();

        return CreatedAtAction("PostRoom", new { id = mappedRoom.Id }, _mapper.Map<RoomPostDto>(mappedRoom));
    }

    /// <summary>
    /// Deletes room from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>No result value or error code</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(uint id)
    {
        if (_context.Rooms == null)
        {
            return NotFound();
        }
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
