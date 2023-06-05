using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotelAppClient;

public class ApiWrapper
{
    private readonly ApiClient _client;

    public ApiWrapper()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var serverUrl = configuration.GetSection("ServerUrl").Value;

        _client = new ApiClient(serverUrl, new HttpClient());
    }

    public async Task<ICollection<ClientGetDto>> GetClientsAsync()
    {
        return await _client.ClientsAllAsync();
    }

    public async Task<ClientGetDto> GetClientAsync(int id)
    {
        return await _client.Clients2Async(id);
    }

    public async Task UpdateClientAsync(int id, ClientPostDto clientToPut)
    {
        await _client.Clients3Async(id, clientToPut);
    }

    public async Task AddClientAsync(ClientPostDto clientToPost)
    {
        await _client.ClientsAsync(clientToPost);
    }

    public async Task DeleteClientAsync(int id)
    {
        await _client.Clients4Async(id);
    }
}
