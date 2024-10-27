using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Prioridades Prioridades)
    {
        if (!await Existe(Prioridades.PrioridadId))
            return await Insertar(Prioridades);
        else
            return await Modificar(Prioridades);
    }

    private async Task<bool> Insertar(Prioridades Prioridades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Prioridades.Add(Prioridades);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prioridades Prioridades)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(Prioridades);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int PrioridadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AnyAsync(p => p.PrioridadId == PrioridadId);
    }

    public async Task<bool> Existe(string? Descripcion, int? PrioridadId = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AnyAsync(p => p.Descripcion == Descripcion);
    }

    public async Task<bool> Existe(int PrioridadId, string? Descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AnyAsync(p => p.PrioridadId !=  PrioridadId && p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Eliminar (int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var prioridades = await contexto.Prioridades
            .Where(p => p.PrioridadId == id)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<Prioridades?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == id);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();

    }

    public async Task<List<Prioridades>> ObtenerListaPrioridades()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Prioridades.AsNoTracking().ToListAsync();
    }

}
