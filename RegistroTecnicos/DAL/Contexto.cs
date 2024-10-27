using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Trabajos> Trabajos { get; set; }

    public DbSet<Prioridades> Prioridades { get; set; }

    public DbSet<Articulos> Articulos { get; set; }

    public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
    {
        new Articulos(){ArticuloId = 1, Descripcion = "Modem", Costo = 700, Precio = 1500, Existencia = 50},
        new Articulos(){ArticuloId = 2, Descripcion = "Cable UTP", Costo = 30, Precio = 70, Existencia = 130},
        new Articulos(){ArticuloId = 3, Descripcion = "Router", Costo = 1000, Precio = 3200, Existencia = 40 }
    });
        base.OnModelCreating(modelBuilder);
    }
}

