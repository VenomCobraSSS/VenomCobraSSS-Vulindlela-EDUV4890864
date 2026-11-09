namespace Vulindlela.Clearance.Api.Models
{
    public class DocumentRule
    {
        public string Id { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string RequiredDocument { get; set; } = string.Empty;
    }
}