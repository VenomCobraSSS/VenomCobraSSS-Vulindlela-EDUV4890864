using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAuthorizationService _authorizationService;

    public AppointmentController(IAppointmentService appointmentService, IAuthorizationService authorizationService)
    {
        _appointmentService = appointmentService;
        _authorizationService = authorizationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppointmentDto dto)
    {
        await _appointmentService.CreateAppointmentAsync(dto);

        return Ok(dto);
    }

    [HttpGet("citizen/{citizenId}")]
    public async Task<IActionResult> GetAppointments(
    string citizenId)
    {
        AuthorizationResult result =
            await _authorizationService.AuthorizeAsync(
                User,
                citizenId,
                "CitizenRecordPolicy");

        if (!result.Succeeded)
        {
            return Forbid();
        }

        var appointments =
            await _appointmentService.GetAppointmentsAsync(citizenId);

        return Ok(appointments);
    }
}