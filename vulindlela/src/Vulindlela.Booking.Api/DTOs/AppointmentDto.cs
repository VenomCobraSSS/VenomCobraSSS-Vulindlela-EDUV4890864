public class AppointmentDto
{
    public string AppointmentSlotId { get; set; } = string.Empty;
    public string CitizenId { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string ServiceType { get; set; } = string.Empty;
}