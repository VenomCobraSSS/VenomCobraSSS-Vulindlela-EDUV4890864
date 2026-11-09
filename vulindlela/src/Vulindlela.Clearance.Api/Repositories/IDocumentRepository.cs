using Vulindlela.Clearance.Api.Models;

namespace Vulindlela.Clearance.Api.Repositories;

public interface IDocumentRepository
{
    Task AddSubmissionAsync(DocumentSubmission submission);
}