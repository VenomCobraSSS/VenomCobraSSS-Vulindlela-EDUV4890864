public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);

    Task<List<Appointment>>GetAppointmentsAsync(string citizenId);
}