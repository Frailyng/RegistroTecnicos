using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TrabajosService(Contexto contexto)
{ 

    public async Task<bool> Existe(int trabajoId)
    {
        return await contexto.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
    }

    public async Task<bool> Insertar(Trabajos trabajo)
    {
        contexto.Trabajos.Add(trabajo);
        await AfectarArticulos(trabajo.TrabajosDetalle.ToArray());
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task AfectarArticulos(TrabajosDetalle[] detalle)
    {
        foreach (var item in detalle)
        {
            var articulo = await contexto.Articulos.SingleAsync(t => t.ArticuloId == item.ArticuloId);
        }
    }

    public async Task<bool> Modificar(Trabajos trabajo)
    {
        contexto.Update(trabajo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajo)
    {
        if (!await Existe(trabajo.TrabajoId))
        {
            return await Insertar(trabajo);
        }
        else
        {
            return await Modificar(trabajo);
        }
    }

    public async Task<Trabajos> Buscar(int id)
    {
        return await contexto.Trabajos
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridad)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TrabajoId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        return await contexto.Trabajos
            .Include(t => t.TrabajosDetalle)
            .Where(t => t.TrabajoId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await contexto.Trabajos
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridad)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}

