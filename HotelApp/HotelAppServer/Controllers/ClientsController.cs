﻿using AutoMapper;
using HotelAppServer.Dto;
using HotelDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAppServer.Controllers;

/// <summary>
/// Client Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> _logger;
    private readonly HotelDomainDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor setting logger, context and mapper
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ClientsController(ILogger<ClientsController> logger, HotelDomainDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns list of all clients
    /// </summary>
    /// <returns>list of all clients</returns>
    [HttpGet]
    public async Task<ActionResult<List<ClientGetDto>>> GetClients()
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }
        return await _mapper.ProjectTo<ClientGetDto>(_context.Clients).ToListAsync();
    }

    /// <summary>
    /// Returns the client by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>client with given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientGetDto>> GetClient(uint id)
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return _mapper.Map<ClientGetDto>(client);
    }

    /// <summary>
    /// Updates a person in repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clientToPut"></param>
    /// <returns>code of operation</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(uint id, [FromBody] ClientPostDto clientToPut)
    {
        if (_context.Clients == null)
            return NotFound();

        var client = await _context.Clients.FindAsync(id);

        if (client == null)
            return NotFound();

        _mapper.Map(clientToPut, client);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Adds a client to repository
    /// </summary>
    /// <param name="clientToPost"></param>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Client>> PostClient(ClientPostDto clientToPost)
    {
        if (_context.Clients == null)
        {
            return Problem("Entity set 'HotelDomainDbContext.Clients'  is null.");
        }

        var mappedClient = _mapper.Map<Client>(clientToPost);
        _context.Clients.Add(mappedClient);

        await _context.SaveChangesAsync();

        return CreatedAtAction("PostClient", new { id = mappedClient.Id }, _mapper.Map<Client>(mappedClient));
    }

    /// <summary>
    /// Deletes client from collection
    /// </summary>
    /// <param name="id"></param>
    /// <returns>code of operation</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(uint id)
    {
        if (_context.Clients == null)
        {
            return NotFound();
        }
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
