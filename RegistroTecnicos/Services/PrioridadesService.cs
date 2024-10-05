using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesService
{
    private readonly Contexto _context;

    public PrioridadesService(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Guardar(Prioridades Prioridades)
    {
        if (!await Existe(Prioridades.PrioridadId))
            return await Insertar(Prioridades);
        else
            return await Modificar(Prioridades);
    }

    private async Task<bool> Insertar(Prioridades Prioridades)
    {
        _context.Prioridades.Add(Prioridades);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prioridades Prioridades)
    {
        _context.Update(Prioridades);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int PrioridadId)
    {
        return await _context.Prioridades
            .AnyAsync(p => p.PrioridadId == PrioridadId);
    }

    public async Task<bool> Existe(string? Descripcion, int? PrioridadId = null)
    {
        return await _context.Prioridades
            .AnyAsync(p => p.Descripcion == Descripcion);
    }

    public async Task<bool> Existe(int PrioridadId, string? Descripcion)
    {
        return await _context.Prioridades
            .AnyAsync(p => p.PrioridadId !=  PrioridadId && p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Eliminar (int id)
    {
        var prioridades = await _context.Prioridades
            .Where(p => p.PrioridadId == id)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<Prioridades?> Buscar(int id)
    {
        return await _context.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == id);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        return await _context.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();

    }

    public async Task<List<Prioridades>> ObtenerListaPrioridades()
    {
        return await _context.Prioridades.AsNoTracking().ToListAsync();
    }

}
