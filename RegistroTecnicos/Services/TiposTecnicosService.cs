using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;


namespace RegistroTecnicos.Services;

public class TiposTecnicosService
{    // Método adicional para obtener una lista predefinida de tipos
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
                Descripcion = "Reparacion",
                Activo = true
            },
            new TiposTecnicos
            {
                TipoTecnicoId = 3,
                Descripcion = "Impresoras",
                Activo = true
            },
            new TiposTecnicos
            {
                TipoTecnicoId = 4,
                Descripcion = "Soporte",
                Activo = true
            },
            new TiposTecnicos
            {
                TipoTecnicoId = 5,
                Descripcion = "Celulares",
                Activo = false
            }
        };
    }
    private readonly Contexto _context;
    public TiposTecnicosService(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Guardar(TiposTecnicos TiposTecnicos)
    {
        if (!await Existe(TiposTecnicos.TipoTecnicoId))
            return await Insertar(TiposTecnicos);
        else
            return await Modificar(TiposTecnicos);
    }

    private async Task<bool> Insertar(TiposTecnicos TiposTecnicos)
    {
        _context.TiposTecnicos.Add(TiposTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(TiposTecnicos TiposTecnicos)
    {
        _context.Update(TiposTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int TipoTecnicoId)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId == TipoTecnicoId);
    }

    public async Task<bool> Existe(string? Descripcion, int? tipotecnicoId = null)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Existe(int tipotecnicoId, string? Descripcion)
    {
        return await _context.TiposTecnicos
            .AnyAsync(p => p.TipoTecnicoId != tipotecnicoId && p.Descripcion.Equals(Descripcion));
    }

    public async Task<bool> Eliminar(int id)
    {
        var tipostecnicos = await _context.TiposTecnicos
            .Where(p => p.TipoTecnicoId == id)
            .ExecuteDeleteAsync();
        return tipostecnicos > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int id)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TipoTecnicoId == id);
    }

    // Aquí va el método Listar modificado
    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _context.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
