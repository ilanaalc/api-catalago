using Microsoft.EntityFrameworkCore;
using CatalagoAPI.Models;

namespace CatalagoAPI.Data.Context
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>(e =>
            {
                e.HasKey(c => c.CategoriaId);

                e.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(80);

                e.Property(c => c.ImagemUrl)
                .IsRequired()
                .HasMaxLength(300);
            });

            modelBuilder.Entity<Produto>(e =>
            {
                e.HasKey(p => p.ProdutoId);

                e.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(80);

                e.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(300);

                e.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

                e.Property(p => p.ImagemUrl)
                .IsRequired()
                .HasMaxLength(80);

                e.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
            });
        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

    }
}
