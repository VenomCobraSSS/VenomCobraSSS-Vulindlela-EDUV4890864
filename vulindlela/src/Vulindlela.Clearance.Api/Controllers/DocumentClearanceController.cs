using Microsoft.AspNetCore.Mvc;
using Vulindlela.Clearance.Api.DTOs;
using Vulindlela.Clearance.Api.Models;
using Vulindlela.Clearance.Api.Services;

namespace Vulindlela.Clearance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentClearanceController : Controller
{
    private readonly IDocumentClearanceService _service;
    private readonly IDocumentQueue _queue;

    public DocumentClearanceController(IDocumentClearanceService service, IDocumentQueue queue)
    {
        _service = service;
        _queue = queue;
    }

    [HttpPost]
    public async Task<IActionResult> Verify(DocumentSubmissionDto dto)
    {
        var result =
            await _service.VerifyAsync(dto);

        return Ok(result);
    }

    //new
    [HttpPost]
    public async Task<IActionResult> Submit(DocumentSubmissionDto dto)
    {
        DocumentSubmission submission = new()
        {
            CitizenId = dto.CitizenId,
            ServiceType = dto.ServiceType
        };

        await _queue.QueueAsync(submission);

        return Accepted();
    }
}