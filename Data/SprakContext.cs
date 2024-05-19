using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Data
{
    public class dbContext : IdentityDbContext<ApplicationUser>
    {
        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {

        }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<ProjectGallery> ProjectGallery { get; set; }

        public DbSet<Offers> Offers { get; set; }

        public DbSet<Keywords> Keywords { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Ratings> Ratings { get; set; }
    }
}
