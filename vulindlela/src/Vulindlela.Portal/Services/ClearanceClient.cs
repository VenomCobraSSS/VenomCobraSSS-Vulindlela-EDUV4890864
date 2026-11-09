namespace Vulindlela.Portal.Services;

public class ClearanceClient
    : IClearanceClient
{
    private readonly HttpClient _httpClient;

    public ClearanceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Dictionary<string, string>> GetStatusesAsync()
    {
        return await _httpClient.GetFromJsonAsync<Dictionary<string, string>>("api/documentclearance") ?? [];
    }
}