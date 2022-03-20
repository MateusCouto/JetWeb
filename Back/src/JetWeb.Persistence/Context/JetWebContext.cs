
using JetWeb.Domain.Entities;
using JetWeb.Domain;
using Microsoft.EntityFrameworkCore;

namespace JetWeb.Persistence.Context
{
    public class JetWebContext : DbContext
    {
        public JetWebContext(DbContextOptions<JetWebContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(P => new { P.Id });
        }
    }
}