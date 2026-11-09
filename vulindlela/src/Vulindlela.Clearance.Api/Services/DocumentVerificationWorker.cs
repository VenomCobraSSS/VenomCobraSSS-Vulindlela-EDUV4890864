namespace Vulindlela.Clearance.Api.Services;

public class DocumentVerificationWorker
    : BackgroundService
{
    private readonly IDocumentQueue _queue;

    public DocumentVerificationWorker(IDocumentQueue queue)
    {
        _queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var submission = await _queue.DequeueAsync(stoppingToken);

            await Task.Delay(3000, stoppingToken);

            Console.WriteLine($"Processed submission {submission.Id}");
        }
    }
}