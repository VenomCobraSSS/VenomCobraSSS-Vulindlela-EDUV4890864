using Vulindlela.Clearance.Api.Data;
using Vulindlela.Clearance.Api.DTOs;

namespace Vulindlela.Clearance.Api.Services;

public class DocumentClearanceService
    : IDocumentClearanceService
{
    private readonly DocumentClearanceDbContext _context;

    public DocumentClearanceService(
        DocumentClearanceDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentClearanceResultDto>
        VerifyAsync(DocumentSubmissionDto dto)
    {
        var requiredDocuments =
            _context.DocumentRules
                    .Where(r =>
                        r.ServiceType ==
                        dto.ServiceType)
                    .Select(r =>
                        r.RequiredDocument)
                    .ToList();

        List<string> missingDocuments = [];

        foreach (var document in requiredDocuments)
        {
            if (!dto.SubmittedDocuments.Contains(document))
            {
                missingDocuments.Add(document);
            }
        }

        return new DocumentClearanceResultDto
        {
            IsCleared = !missingDocuments.Any(),
            MissingDocuments = missingDocuments
        };
    }
}