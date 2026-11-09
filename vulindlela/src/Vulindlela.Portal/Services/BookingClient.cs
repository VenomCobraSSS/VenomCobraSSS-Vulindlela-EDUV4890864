using Vulindlela.Portal.Models;

namespace Vulindlela.Portal.Services;

public class BookingClient : IBookingClient
{
    private readonly HttpClient _httpClient;

    public BookingClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AppointmentViewModel>> GetAppointmentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AppointmentViewModel>>("api/appointment") ?? [];
    }
}