using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Trabajos> Trabajos { get; set; }

        public DbSet<Prioridades> Priidades { get; set; }

        // Configuración de relaciones si es necesario
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones si es necesario
            modelBuilder.Entity<Trabajos>()
                .HasOne(t => t.Cliente)
                .WithMany(c => c.Trabajos)
                .HasForeignKey(t => t.ClienteId);

            modelBuilder.Entity<Trabajos>()
                .HasOne(t => t.Tecnico)
                .WithMany(te => te.Trabajos)
                .HasForeignKey(t => t.TecnicoId);
        }
    }
}

