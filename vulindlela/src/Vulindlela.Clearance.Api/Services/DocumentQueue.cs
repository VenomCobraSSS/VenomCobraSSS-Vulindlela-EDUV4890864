using System.Threading.Channels;
using Vulindlela.Clearance.Api.Models;

namespace Vulindlela.Clearance.Api.Services;
public class DocumentQueue : IDocumentQueue
{
    private readonly Channel<DocumentSubmission> _channel;

    public DocumentQueue()
    {
        _channel = Channel.CreateUnbounded<DocumentSubmission>();
    }

    public async ValueTask QueueAsync(DocumentSubmission submission)
    {
        await _channel.Writer.WriteAsync(submission);
    }

    public async ValueTask<DocumentSubmission> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}