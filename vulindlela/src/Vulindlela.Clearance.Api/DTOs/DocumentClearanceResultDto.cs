namespace Vulindlela.Clearance.Api.DTOs
{
    public class DocumentClearanceResultDto
    {
        public bool IsCleared { get; set; }
        public List<string> MissingDocuments { get; set; } = [];
    }
}
