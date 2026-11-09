namespace Vulindlela.Booking.Api.Services;
public class ClearanceClient : IClearanceClient
{
    private readonly HttpClient _httpClient;

    public ClearanceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task NotifyAsync(ClearanceRequestDto request)
    {
        await _httpClient.PostAsJsonAsync("api/documentclearance", request);
    }
}