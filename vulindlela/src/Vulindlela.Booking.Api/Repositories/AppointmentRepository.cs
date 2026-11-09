using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly BookingDbContext _context;

    public AppointmentRepository(BookingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(string citizenId)
    {
        return await _context.Appointments.Where(a => a.CitizenId == citizenId).ToListAsync();
    }
}