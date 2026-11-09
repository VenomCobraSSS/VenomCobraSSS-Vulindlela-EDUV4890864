namespace Vulindlela.Clearance.Api.DTOs
{
    public class DocumentSubmissionDto
    {
        public string CitizenId { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public List<string> SubmittedDocuments { get; set; } = [];
    }
}
