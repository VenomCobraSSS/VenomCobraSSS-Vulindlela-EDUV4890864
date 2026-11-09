namespace Vulindlela.Booking.Api.Services;
public interface IClearanceClient
{
    Task NotifyAsync(ClearanceRequestDto request);
}