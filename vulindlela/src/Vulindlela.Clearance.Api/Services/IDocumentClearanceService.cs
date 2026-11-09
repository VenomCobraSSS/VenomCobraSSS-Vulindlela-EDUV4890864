using Vulindlela.Clearance.Api.DTOs;

namespace Vulindlela.Clearance.Api.Services
{
    public interface IDocumentClearanceService
    {
        Task<DocumentClearanceResultDto> VerifyAsync(DocumentSubmissionDto dto);
    }
}
