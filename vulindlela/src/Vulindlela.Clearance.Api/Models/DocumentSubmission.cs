namespace Vulindlela.Clearance.Api.Models
{
    public class DocumentSubmission
    {
        public int Id { get; set; }
        public string CitizenId { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public bool IsCleared { get; set; }
    }
}
