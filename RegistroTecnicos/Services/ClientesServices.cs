using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class ClientesServices(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<bool> Guardar(Clientes Clientes)
    {
        if (!await Existe(Clientes.ClienteId))
            return await Insertar(Clientes);
        else
            return await Modificar(Clientes);
    }

    private async Task<bool> Insertar(Clientes Clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(Clientes);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes Clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(Clientes);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int ClienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(p => p.ClienteId == ClienteId);
    }

    public async Task<bool> Existe(string? Nombres, int? ClienteId = null)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(p => p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Existe(int ClienteId, string? Nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(p => p.ClienteId != ClienteId && p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var Clientes = await contexto.Clientes
            .Where(p => p.ClienteId == id)
            .ExecuteDeleteAsync();
        return Clientes > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ClienteId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Clientes>> ObtenerListaClientes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking().ToListAsync();
    }
}
