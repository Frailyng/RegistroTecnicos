using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Tecnicos Tecnicos)
    {
        if (!await Existe(Tecnicos.TecnicoId))
            return await Insertar(Tecnicos);
        else
            return await Modificar(Tecnicos);
    }

    private async Task<bool> Insertar(Tecnicos Tecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(Tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos Tecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(Tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int TecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(p => p.TecnicoId == TecnicoId);
    }

    public async Task<bool> Existe(string? Nombres, int? tecnicoId = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(p => p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Existe(int tecnicoId, string? Nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(p => p.TecnicoId != tecnicoId && p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var tecnicos = await contexto.Tecnicos
            .Where(p => p.TecnicoId == id)
            .ExecuteDeleteAsync();
        return tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TecnicoId == id);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Tecnicos>> ObtenerListaTecnicos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.AsNoTracking().ToListAsync();
    }
}
