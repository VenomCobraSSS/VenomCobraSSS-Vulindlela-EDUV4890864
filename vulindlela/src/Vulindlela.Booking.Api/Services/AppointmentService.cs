using Microsoft.EntityFrameworkCore;

namespace Vulindlela.Booking.Api.Services;
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly IClearanceClient _clearanceClient;

    public AppointmentService(IAppointmentRepository repository, IClearanceClient clearanceClient)
    {
        _repository = repository;
        _clearanceClient = clearanceClient;
    }

    public async Task CreateAppointmentAsync(AppointmentDto dto)
    {
        if (dto.AppointmentDate < DateTime.Today)
        {
            throw new Exception("Booking date cannot be in the past!");
        }

        Appointment appointment = new Appointment
        {
            AppointmentSlotId = dto.AppointmentSlotId,
            CitizenId = dto.CitizenId,
            AppointmentDate = dto.AppointmentDate,
            ServiceType = dto.ServiceType,
        };

        try
        {
            await _repository.AddAsync(appointment);
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("This slot has already been booked.");
        }

        try
        {
            await _clearanceClient.NotifyAsync(new ClearanceRequestDto
            {
                CitizenId = dto.CitizenId,
                ServiceType = dto.ServiceType
            });
        }
        catch (Exception e)
        {
            Console.WriteLine($"Clearance service unavailable: {e.Message}");
        }
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(
        string citizenId)
    {
        return await _repository.GetAppointmentsAsync(citizenId);
    }
}