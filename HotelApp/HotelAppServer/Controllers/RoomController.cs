using AutoMapper;
using HotelAppServer.Dto;
using HotelAppServer.Repository;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;

namespace HotelAppServer.Controllers;

/// <summary>
/// Room controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IMapper _mapper;
    private readonly IHotelAppRepository _hotelAppRepository;

    /// <summary>
    /// Constructor setting logger, data repository and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="hotelAppRepository"></param>
    /// <param name="mapper"></param>
    public RoomController(ILogger<RoomController> logger, IMapper mapper, IHotelAppRepository hotelAppRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _hotelAppRepository = hotelAppRepository;
    }

    /// <summary>
    /// Returns list of all rooms
    /// </summary>
    /// <returns>list of all rooms</returns>
    [HttpGet]
    public List<Room> Get()
    {
        _logger.LogInformation("GET list of all rooms");
        return _hotelAppRepository.Rooms;
    }

    /// <summary>
    /// Returns room with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>room with given id</returns>
    [HttpGet("{id}")]
    public ActionResult<Room> Get(int id)
    {
        _logger.LogInformation("GET room with id = {Id}", id);
        var room = _hotelAppRepository.Rooms.FirstOrDefault(room => room.Id == id);

        if (room == null)
        {
            _logger.LogInformation("Not found room with id={Id}", id);
            return NotFound();
        }

        return Ok(room);
    }

    /// <summary>
    /// Adds room to repository's list of rooms
    /// </summary>
    /// <param name="roomToPost"></param>
    [HttpPost]
    public void Post([FromBody] RoomPostDto roomToPost)
    {
        _logger.LogInformation("POST room: {Type}, {Amount}, {Cost}", roomToPost.Type, roomToPost.Amount, roomToPost.Cost);
        _hotelAppRepository.Rooms.Add(_mapper.Map<Room>(roomToPost));
    }

    /// <summary>
    /// Updates the room in list of rooms
    /// </summary>
    /// <param name="id"></param>
    /// <param name="roomToPut"></param>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] RoomPostDto roomToPut)
    {
        _logger.LogInformation("PUT room with id={id}: {Type}, {Amount}, {Cost}", id, roomToPut.Type, roomToPut.Amount, roomToPut.Cost);

        var room = _hotelAppRepository.Rooms.FirstOrDefault(room => room.Id == id);

        if (room == null)
        {
            _logger.LogInformation("Not found room with id={Id}", id);
            return NotFound();
        }

        room = _mapper.Map(roomToPut, room);
        return Ok();
    }

    /// <summary>
    /// Deletes room from repository's room list
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("DELETE room with id={Id}", id);

        var room = _hotelAppRepository.Rooms.FirstOrDefault(room => room.Id == id);

        if (room == null)
        {
            _logger.LogInformation("Not found room with id={Id}", id);
            return NotFound();
        }

        _hotelAppRepository.Rooms.Remove(room);
        return Ok();
    }
}
