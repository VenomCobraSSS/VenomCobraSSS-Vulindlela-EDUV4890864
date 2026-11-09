namespace Vulindlela.Portal.Services;

public interface IClearanceClient
{
    Task<Dictionary<string, string>> GetStatusesAsync();
}
