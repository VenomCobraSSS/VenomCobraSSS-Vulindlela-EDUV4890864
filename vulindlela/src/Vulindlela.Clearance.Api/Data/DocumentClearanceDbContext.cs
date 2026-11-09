using Microsoft.EntityFrameworkCore;
using Vulindlela.Clearance.Api.Models;

namespace Vulindlela.Clearance.Api.Data;

public class DocumentClearanceDbContext : DbContext
{
    public DocumentClearanceDbContext(
        DbContextOptions<DocumentClearanceDbContext> options)
        : base(options)
    {
    }

    public DbSet<DocumentSubmission> DocumentSubmissions { get; set; }

    public DbSet<DocumentRule> DocumentRules { get; set; }
}