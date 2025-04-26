using Microsoft.EntityFrameworkCore;
using WebAPI_ASPNET_Core.Models;

namespace WebAPI_ASPNET_Core.Data;

public class WACContext : DbContext
{
    public WACContext(DbContextOptions<WACContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DocumentV1> DocumentV1 { get; set; }
}