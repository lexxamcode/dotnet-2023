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
    /// <param name="logger">logger</param>
    /// <param name="context">context</param>
    /// <param name="mapper">mapper</param>
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
        _logger.LogInformation("Get all rooms");
        if (_context.Rooms == null)
        {   
            _logger.LogInformation("Rooms list is empty");
            return NotFound();
        }
        return await _mapper.ProjectTo<RoomGetDto>(_context.Rooms).ToListAsync();
    }

    /// <summary>
    /// Gets room by its id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Room with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomGetDto>> GetRoom(uint id)
    {
        if (_context.Rooms == null)
        {
            _logger.LogInformation("Rooms list is empty");
            return NotFound();
        }
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            _logger.LogInformation("Can not find room with id={Id}", id);
            return NotFound();
        }

        return _mapper.Map<RoomGetDto>(room);
    }

    /// <summary>
    /// Updates existing room in database
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="roomToPut">room</param>
    /// <returns>No return value or error code</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(uint id, RoomPostDto roomToPut)
    {
        _logger.LogInformation("Put room with id={Id}", id);
        if (_context.Rooms == null)
        {
            _logger.LogInformation("Rooms list is empty");
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            _logger.LogInformation("Can not find room with id={Id}", id);
            return NotFound();
        }

        _mapper.Map(roomToPut, room);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a new room to the database
    /// </summary>
    /// <param name="room">room</param>
    /// <returns>New room instance</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Room>> PostRoom(RoomPostDto room)
    {
        _logger.LogInformation("Post new room");

        if (_context.Rooms == null)
        {
            _logger.LogInformation("Rooms list is empty");
            return NotFound();
        }

        var mappedRoom = _mapper.Map<Room>(room);
        _context.Rooms.Add(mappedRoom);
        await _context.SaveChangesAsync();

        return CreatedAtAction("PostRoom", new { id = mappedRoom.Id }, _mapper.Map<RoomPostDto>(mappedRoom));
    }

    /// <summary>
    /// Deletes room from database
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>No result value or error code</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(uint id)
    {
        _logger.LogInformation("Delete room with id={Id}", id);
        if (_context.Rooms == null)
        {
            _logger.LogInformation("Rooms list is empty");
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            _logger.LogInformation("Can not find room with id={Id}", id);
            return NotFound();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
