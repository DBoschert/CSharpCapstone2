using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSharpCapstone.Models;

namespace CSharpCapstone.Data
{
    public class CSharpCapstoneContext : DbContext
    {
        public CSharpCapstoneContext (DbContextOptions<CSharpCapstoneContext> options)
            : base(options)
        {
        }

        public DbSet<CSharpCapstone.Models.User> Users { get; set; } = default!;

        public DbSet<CSharpCapstone.Models.Vendor> Vendors { get; set; } = default!;

        public DbSet<CSharpCapstone.Models.Product> Products { get; set; } = default!;

        public DbSet<CSharpCapstone.Models.Request> Requests { get; set; } = default!;

        public DbSet<CSharpCapstone.Models.RequestLine> RequestLines { get; set; } = default!;
    }
}
