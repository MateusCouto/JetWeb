using JetWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JetWeb.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(P => new { P.ProdutoId });
        }
    }
}