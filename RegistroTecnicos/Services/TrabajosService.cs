using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TrabajosService
{
    private readonly Contexto _context;

    public TrabajosService(Contexto context)
    {
        _context = context;
    }

    public async Task<bool> Guardar(Trabajos Trabajos)
    {
        if (!await Existe(Trabajos.TrabajoId))
            return await Insertar(Trabajos);
        else
            return await Modificar(Trabajos);
    }

    private async Task<bool> Insertar(Trabajos Trabajos)
    {
        _context.Trabajos.Add(Trabajos);
        return await _context.SaveChangesAsync() > 0;

    }

    private async Task <bool> Modificar(Trabajos Trabajos)
    {
        _context.Update(Trabajos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task <bool> Existe(int TrabajoId)
    {
        return await _context.Trabajos
            .AnyAsync(p => p.TrabajoId == TrabajoId);
    }

    public async Task<bool> Existe(DateTime? Fecha,  int? TrabajoId = null)
    {
        return await _context.Trabajos
            .AnyAsync(p => p.Fecha == Fecha);
    }

    public async Task<bool> Existe(int TrabajoId, DateTime? Fecha)
    {
        return await _context.Trabajos
            .AnyAsync(p => p.TrabajoId != TrabajoId && p.Fecha.Equals(Fecha));
    }

    public async Task<bool> Eliminar(int id)
    {
        var trabajosEliminados = await _context.Trabajos
            .Where(p => p.TrabajoId == id)
            .ExecuteDeleteAsync();
        return trabajosEliminados > 0;
    }


    public async Task<Trabajos?> Buscar(int id)
    {
        return await _context.Trabajos
            .Include(t => t.Cliente)   
            .Include(t => t.Tecnico)   
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TrabajoId == id);
    }


    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _context.Trabajos
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }


}
