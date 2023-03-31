using AutoMapper;
using HotelAppServer.Dto;
using HotelAppServer.Repository;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;

namespace HotelAppServer.Controllers;

/// <summary>
/// Client Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IMapper _mapper;
    private readonly IHotelAppRepository _hotelAppRepository;
    /// <summary>
    /// Constructor setting logger, data repository and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="hotelAppRepository"></param>
    /// <param name="mapper"></param>
    public ClientController(ILogger<ClientController> logger, IHotelAppRepository hotelAppRepository, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _hotelAppRepository = hotelAppRepository;
    }
    /// <summary>
    /// Returns list of all clients
    /// </summary>
    /// <returns>list of all clients</returns>
    [HttpGet]
    public List<Client> Get()
    {
        _logger.LogInformation("GET clients");
        return _hotelAppRepository.Clients;
    }

    /// <summary>
    /// Returns the client by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>client with given id</returns>
    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        _logger.LogInformation("GET client with id={Id}", id);
        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);
        if (client == null)
        {
            _logger.LogInformation("Not found client with id={Id}", id);
            return NotFound();
        }
        else
            return Ok(client);
    }

    /// <summary>
    /// Adds a client to repository
    /// </summary>
    /// <param name="clientToPost"></param>
    [HttpPost]
    public void Post([FromBody] ClientPostDto clientToPost)
    {
        _logger.LogInformation("POST client {FirstName}, {LastName}, {Surname}, {Passport}",
                               clientToPost.FirstName,
                               clientToPost.LastName,
                               clientToPost.Surname,
                               clientToPost.Passport);
        _hotelAppRepository.Clients.Add(_mapper.Map<Client>(clientToPost));
    }

    /// <summary>
    /// Updates a person in repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clientToPut"></param>
    /// <returns>code of operation</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientPostDto clientToPut)
    {
        _logger.LogInformation("PUT to client {Id}: {FirstName}, {LastName}, {Surname}, {Passport}",
                               id,
                               clientToPut.FirstName,
                               clientToPut.LastName,
                               clientToPut.Surname,
                               clientToPut.Passport);

        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);

        if (client == null)
        {
            _logger.LogInformation("Not found client with id={Id}", id);
            return NotFound();
        }
        else
        {
            client = _mapper.Map(clientToPut, client);
            return Ok();
        }
    }

    /// <summary>
    /// Deletes client from collection
    /// </summary>
    /// <param name="id"></param>
    /// <returns>code of operation</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("DELETE client with id={Id}", id);
        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);

        if (client == null)
        {
            _logger.LogInformation("Not found client with id={Id}", id);
            return NotFound();
        }
        else
        {
            _hotelAppRepository.Clients.Remove(client);
            return Ok();
        }
    }
}
