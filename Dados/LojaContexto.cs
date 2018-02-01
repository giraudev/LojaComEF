using LojaWebEF.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaWebEF.Dados
{
    public class LojaContexto : DbContext
    {
        public LojaContexto(DbContextOptions<LojaContexto> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
        }
    }
}