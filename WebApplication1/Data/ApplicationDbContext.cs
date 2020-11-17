using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace GitHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Repositorio> Repositorios { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Repositorio>()
                   .HasOne(x => x.owner)
                   .WithOne(x => x.Repositorio)
                   .HasForeignKey<Owner>(x => x.RepositorioId);
        }
    }
}
