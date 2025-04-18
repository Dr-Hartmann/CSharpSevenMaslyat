using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Data
{
    public class AspNetCoreMVCContext : DbContext
    {
        public AspNetCoreMVCContext (DbContextOptions<AspNetCoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetCoreMVC.Models.Movies> Movies { get; set; } = default!;
    }
}
