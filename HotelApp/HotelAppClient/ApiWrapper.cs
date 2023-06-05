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

    public async Task<ICollection<HotelGetDto>> GetHotelsAsync()
    {
        return await _client.HotelsAllAsync();
    }
    
    public async Task<HotelGetDto> GetHotelAsync(int id)
    {
        return await _client.Hotels2Async(id);
    }

    public async Task UpdateHotelAsync(int id, HotelPostDto hotelToPut)
    {
        await _client.Hotels3Async(id, hotelToPut);
    }

    public async Task AddHotelAsync(HotelPostDto hotelToPost)
    {
        await _client.HotelsAsync(hotelToPost);
    }

    public async Task DeleteHotelAsync(int id)
    {
        await _client.Hotels4Async(id);
    }

    public async Task<ICollection<RoomGetDto>> GetRoomsAsync()
    {
        return await _client.RoomsAllAsync();
    }

    public async Task<RoomGetDto> GetRoomAsync(int id)
    {
        return await _client.Rooms2Async(id);
    }

    public async Task UpdateRoomAsync(int id, RoomPostDto roomToPut)
    {
        await _client.Rooms3Async(id,roomToPut);
    }

    public async Task AddRoomAsync(RoomPostDto roomToPost)
    {
        await _client.RoomsAsync(roomToPost);
    }

    public async Task DeleteRoomAsync(int id)
    {
        await _client.Rooms4Async(id);
    }

    public async Task<ICollection<BookingGetDto>> GetBookingsAsync()
    {
        return await _client.BookingsAllAsync();
    }

    public async Task<BookingGetDto> GetBookingAsync(int id)
    {
        return await _client.Bookings2Async(id);
    }

    public async Task UpdateBookingAsync(int id, BookingPostDto bookingToPut)
    {
        await _client.Bookings3Async(id, bookingToPut);
    }

    public async Task AddBookingAsync(BookingPostDto bookingToPost)
    {
        await _client.BookingsAsync(bookingToPost);
    }

    public async Task DeleteBookingAsync(int id)
    {
        await _client.Bookings4Async(id);
    }
}
