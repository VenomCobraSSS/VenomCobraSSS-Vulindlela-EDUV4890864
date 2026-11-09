using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Vulindlela.Booking.Api.Authorisation;

public class CitizenRecordHandler
    : AuthorizationHandler<CitizenRecordRequirement, string>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CitizenRecordRequirement requirement,
        string citizenId)
    {
        if (context.User.IsInRole("Official"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        string? currentUserId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (currentUserId == citizenId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}