public interface IAppointmentService
{
    Task CreateAppointmentAsync(AppointmentDto dto);

    Task<List<Appointment>> GetAppointmentsAsync(string citizenId);
}