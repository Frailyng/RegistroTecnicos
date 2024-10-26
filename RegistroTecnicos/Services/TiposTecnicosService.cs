using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;


namespace RegistroTecnicos.Services;

public class TiposTecnicosService(IDbContextFactory<Contexto> DbFactory)
{   
    public async Task<bool> Guardar(TiposTecnicos TiposTecnicos)
    {
        if (!await Existe(TiposTecnicos.TipoTecnicoId))
            return await Insertar(TiposTecnicos);
        else
            return await Modificar(TiposTecnicos);
    }

    private async Task<bool> Insertar(TiposTecnicos TiposTecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.TiposTecnicos.Add(TiposTecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos TiposTecnicos)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(TiposTecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int TipoTecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId == TipoTecnicoId);
    }

    public async Task<bool> Existe(string? Descripcion, int? tipotecnicoId = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AnyAsync(p => p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Existe(int tipotecnicoId, string? Descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId != tipotecnicoId && p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var tipostecnicos = await contexto.TiposTecnicos
            .Where(p => p.TipoTecnicoId == id)
            .ExecuteDeleteAsync();
        return tipostecnicos > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TipoTecnicoId == id);
    }

    // Aquí va el método Listar modificado
    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
