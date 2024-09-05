using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TiposTecnicosService
{
    private readonly Contexto _context;
    public TiposTecnicosService(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Guardar(TiposTecnicos tipos)
    {
        if (!await Existe(tipos.TipoTecnicoId))
            return await Insertar(tipos);
        else
            return await Modificar(tipos);
    }

    private async Task<bool> Insertar(TiposTecnicos tipos)
    {
        _context.TiposTecnicos.Add(tipos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos tipos)
    {
        _context.Update(tipos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId == id);
    }

    public async Task<bool> Existe(string? descripcion, int? id = null)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.Descripcion.Equals(descripcion));
    }

    public async Task<bool> Existe(int id, string? descripcion)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId != id && p.Descripcion.Equals(descripcion));
    }

    public async Task<bool> Eliminar(int id)
    {
        var tipos = await _context.TiposTecnicos
            .Where(p => p.TipoTecnicoId == id)
            .ExecuteDeleteAsync();
        return tipos > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TipoTecnicoId == id);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Método adicional para obtener una lista predefinida de tipos
    public List<TiposTecnicos> ObtenerTiposPredefinidos()
    {
        return new List<TiposTecnicos>
        {
            new TiposTecnicos
            {
                TipoTecnicoId = 1,
                Descripcion = "Redes",
                Activo = true
            },
            new TiposTecnicos
            {
                TipoTecnicoId = 2,
                Descripcion = "Impresora",
                Activo = true
            }
        };
    }
}
