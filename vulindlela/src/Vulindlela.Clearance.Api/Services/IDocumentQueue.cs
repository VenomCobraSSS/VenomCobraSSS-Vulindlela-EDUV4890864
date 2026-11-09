using Vulindlela.Clearance.Api.Models;

namespace Vulindlela.Clearance.Api.Services
{
    public interface IDocumentQueue
    {
        ValueTask QueueAsync(DocumentSubmission submission);

        ValueTask<DocumentSubmission> DequeueAsync(CancellationToken cancellationToken);
    }
}
