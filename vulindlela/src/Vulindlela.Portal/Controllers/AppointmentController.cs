using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vulindlela.Portal.Models;
using Vulindlela.Portal.Services;

namespace Vulindlela.Portal.Controllers;

[Authorize]
public class AppointmentController : Controller
{
    private readonly IBookingClient _bookingClient;
    private readonly IClearanceClient _clearanceClient;

    public AppointmentController(IBookingClient bookingClient, IClearanceClient clearanceClient)
    {
        _bookingClient = bookingClient;
        _clearanceClient = clearanceClient;
    }

    public async Task<IActionResult> Index()
    {
        List<AppointmentViewModel> appointments = await _bookingClient.GetAppointmentsAsync();

        try
        {
            var statuses = await _clearanceClient.GetStatusesAsync();

            foreach (var appointment in appointments)
            {
                if (statuses.ContainsKey(appointment.CitizenId))
                {
                    appointment.ClearanceStatus = statuses[appointment.CitizenId];
                }
            }
        }
        catch
        {
            ViewBag.Warning = "Document clearance information is currently unavailable.";
        }

        return View(appointments);
    }
}