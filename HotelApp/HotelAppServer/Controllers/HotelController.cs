using AutoMapper;
using HotelAppServer.Dto;
using HotelAppServer.Repository;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;

namespace HotelAppServer.Controllers;

/// <summary>
/// Hotel controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly ILogger<HotelController> _logger;
    private readonly IMapper _mapper;
    private readonly IHotelAppRepository _hotelAppRepositpry;

    /// <summary>
    /// Constructor setting logger, data repository and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="hotelAppRepository"></param>
    public HotelController(ILogger<HotelController> logger, IMapper mapper, IHotelAppRepository hotelAppRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _hotelAppRepositpry = hotelAppRepository;
    }

    /// <summary>
    /// Returns list of hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public List<HotelGetDto> Get()
    {
        _logger.LogInformation("GET all hotels");
        return _hotelAppRepositpry.Hotels.Select(hotel => _mapper.Map<HotelGetDto>(hotel)).ToList();
    }

    /// <summary>
    /// Returns hotel with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<HotelGetDto> Get(int id)
    {
        _logger.LogInformation("GET hotel with id = {Id}", id);
        var hotel = _hotelAppRepositpry.Hotels.FirstOrDefault(hotel => hotel.Id == id);

        if (hotel == null)
        {
            _logger.LogInformation("Not found hotel with id = {Id}", id);
            return NotFound();
        }

        return Ok(hotel);
    }

    /// <summary>
    /// Adds new hotel to repository
    /// </summary>
    /// <param name="hotelToPost"></param>
    [HttpPost]
    public void Post([FromBody] HotelPostDto hotelToPost)
    {
        _logger.LogInformation("POST hotel: {Name}, {City}, {Address}", hotelToPost.Name, hotelToPost.City, hotelToPost.Address);
        _hotelAppRepositpry.Hotels.Add(_mapper.Map<Hotel>(hotelToPost));
    }

    /// <summary>
    /// Updates hotel in repository by its id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="hotelToPut"></param>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] HotelPostDto hotelToPut)
    {
        _logger.LogInformation("PUT hotel with id= {Id}: {Name}, {City}, {Address}", id, hotelToPut.Name, hotelToPut.City, hotelToPut.Address);
        var hotel = _hotelAppRepositpry.Hotels.FirstOrDefault(hotel => hotel.Id == id);

        if (hotel == null)
        {
            _logger.LogInformation("Not found hotel with id = {Id}", id);
            return NotFound();
        }

        hotel = _mapper.Map(hotelToPut, hotel);
        _logger.LogInformation("{Id}, {Name}, {City}, {Address}", hotel.Id, hotel.Name, hotel.City, hotel.Address);
        return Ok();
    }

    /// <summary>
    /// Deletes hotel with given id from repository
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var hotel = _hotelAppRepositpry.Hotels.FirstOrDefault(hotel => hotel.Id == id);

        if (hotel == null)
        {
            _logger.LogInformation("Not found hotel with id = {Id}", id);
            return NotFound();
        }

        _hotelAppRepositpry.Hotels.Remove(hotel);
        return Ok();
    }
}
