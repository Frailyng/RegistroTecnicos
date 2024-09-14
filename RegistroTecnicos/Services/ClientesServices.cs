using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class ClientesServices
{
    private readonly Contexto _context;

    public ClientesServices(Contexto context)
    {
        _context = context;
    }

    public async Task<bool> Guardar(Clientes Clientes)
    {
        if (!await Existe(Clientes.ClienteId))
            return await Insertar(Clientes);
        else
            return await Modificar(Clientes);
    }

    private async Task<bool> Insertar(Clientes Clientes)
    {
        _context.Clientes.Add(Clientes);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes Clientes)
    {
        _context.Update(Clientes);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int ClienteId)
    {
        return await _context.Clientes
            .AnyAsync(p => p.ClienteId == ClienteId);
    }

    public async Task<bool> Existe(string? Nombres, int? ClienteId = null)
    {
        return await _context.Clientes
            .AnyAsync(p => p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Existe(int ClienteId, string? Nombres)
    {
        return await _context.Clientes
            .AnyAsync(p => p.ClienteId != ClienteId && p.Nombres.Equals(Nombres));
    }

    public async Task<bool> Eliminar(int id)
    {
        var Clientes = await _context.Clientes
            .Where(p => p.ClienteId == id)
            .ExecuteDeleteAsync();
        return Clientes > 0;
    }

    public async Task<Clientes?> Buscar(int id)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ClienteId == id);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
    public async Task<List<Clientes>> ObtenerListaClientes()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }
}
