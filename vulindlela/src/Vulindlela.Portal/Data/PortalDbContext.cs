using Microsoft.EntityFrameworkCore;
using Vulindlela.Portal.Models;

namespace Vulindlela.Portal.Data;

public class PortalDbContext : DbContext
{
    public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}