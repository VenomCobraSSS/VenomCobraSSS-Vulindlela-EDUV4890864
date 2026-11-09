namespace Vulindlela.Portal.Models;

public class AppointmentViewModel
{
    public string CitizenId { get; set; } = string.Empty;

    public string ServiceType { get; set; } = string.Empty;

    public DateTime AppointmentDate { get; set; }

    public string ClearanceStatus { get; set; } = string.Empty;
}