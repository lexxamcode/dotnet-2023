using Microsoft.AspNetCore.Mvc;
using HotelDomain;
using HotelAppServer.Dto;
using Microsoft.OpenApi.Validations;
using HotelAppServer.Repository;

namespace HotelAppServer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IHotelAppRepository _hotelAppRepository;

    public ClientController(ILogger<WeatherForecastController> logger, IHotelAppRepository hotelAppRepository)
    {
        _logger = logger;
        _hotelAppRepository = hotelAppRepository;
    }

    [HttpGet]
    public List<Client> Get()
    {
        _logger.LogInformation("GET clients");
        return _hotelAppRepository.Clients;
    }

    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        _logger.LogInformation($"GET client with id={id}");
        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);
        if (client == null)
        {
            _logger.LogInformation($"Not found client with id={id}");
            return NotFound();
        }
        else
            return Ok(client);
    }

    [HttpPost]
    public void Post([FromBody] ClientPostDto client)
    {
        _logger.LogInformation($"POST client {client.FirstName}, {client.LastName}, {client.Surname}, {client.Passport}");
        _hotelAppRepository.Clients.Add(new Client
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Surname = client.Surname,
            Passport = client.Passport,
            BirthDate = client.BirthDate
        });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientPostDto clientToPut)
    {
        _logger.LogInformation($"PUT to client {id}: {clientToPut.FirstName}, {clientToPut.LastName}, {clientToPut.Surname}, {clientToPut.Passport}");
        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);

        if (client == null)
        {
            _logger.LogInformation($"Not found client with id={id}");
            return NotFound();
        }
        else
        {
            client.FirstName = clientToPut.FirstName;
            client.LastName = clientToPut.FirstName;
            client.Surname = clientToPut.Surname;
            client.Passport = clientToPut.Passport;
            client.BirthDate = clientToPut.BirthDate;

            return Ok();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation($"DELETE client with id={id}");
        var client = _hotelAppRepository.Clients.FirstOrDefault(client => client.Id == id);

        if (client == null)
        {
            _logger.LogInformation($"Not found client with id={id}");
            return NotFound();
        }
        else
        {
            _hotelAppRepository.Clients.Remove(client);
            return Ok();
        }
    }
}
