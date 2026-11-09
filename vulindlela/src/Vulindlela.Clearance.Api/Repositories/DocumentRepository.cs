using Vulindlela.Clearance.Api.Data;
using Vulindlela.Clearance.Api.Models;

namespace Vulindlela.Clearance.Api.Repositories;

public class DocumentRepository
    : IDocumentRepository
{
    private readonly
        DocumentClearanceDbContext _context;

    public DocumentRepository(
        DocumentClearanceDbContext context)
    {
        _context = context;
    }

    public async Task AddSubmissionAsync(
        DocumentSubmission submission)
    {
        _context.DocumentSubmissions
            .Add(submission);

        await _context.SaveChangesAsync();
    }
}