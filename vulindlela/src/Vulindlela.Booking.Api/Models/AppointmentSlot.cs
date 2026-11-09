public class AppointmentSlot
{
    public string Id { get; set; }
    public string BranchId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsBooked { get; set; }
}