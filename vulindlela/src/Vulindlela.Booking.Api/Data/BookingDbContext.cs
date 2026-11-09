using Microsoft.EntityFrameworkCore;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasIndex(a => a.AppointmentSlotId)
            .IsUnique();
    }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<AppointmentSlot> AppointmentSlots { get; set; }
}