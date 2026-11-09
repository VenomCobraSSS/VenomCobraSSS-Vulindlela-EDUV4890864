using Vulindlela.Portal.Models;

namespace Vulindlela.Portal.Services;

public interface IBookingClient
{
    Task<List<AppointmentViewModel>> GetAppointmentsAsync();
}
