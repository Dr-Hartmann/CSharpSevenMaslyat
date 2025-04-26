using Microsoft.EntityFrameworkCore;
using WebAPI_ASPNET_Core.Models;

namespace WebAPI_ASPNET_Core.Data
{
    public class TemplateDbContext : DbContext
    {
        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
        {
        }

        public DbSet<Template> Templates { get; set; }
    }
}