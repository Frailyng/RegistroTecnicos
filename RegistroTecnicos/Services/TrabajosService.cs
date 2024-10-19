﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TrabajosService(Contexto contexto)
{
    public async Task<bool> Existe(int TrabajoId)
    {
        return await contexto.Trabajos
            .AnyAsync(p => p.TrabajoId == TrabajoId);
    }

    public async Task<bool> Insertar(Trabajos Trabajos)
    {
        contexto.Trabajos.Add(Trabajos);
        await AfectarTrabajos(Trabajos.TrabajosDetalle.ToArray());
         return await contexto.SaveChangesAsync() > 0;

    }

    public async Task AfectarTrabajos(TrabajosDetalle[] detalle)
    {
        foreach (var item in detalle)
        {
            var trabajo = await contexto.Trabajos.SingleAsync(t => t.TrabajoId == item.TrabajoId);
        }
    }

    public async Task<bool> Modificar(Trabajos Trabajos)
    {
        contexto.Update(Trabajos);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Trabajos Trabajos)
    {
        if (!await Existe(Trabajos.TrabajoId))
            return await Insertar(Trabajos);
        else
            return await Modificar(Trabajos);
    }

    public async Task<Trabajos> Buscar(int id)
    {
        return await contexto.Trabajos
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridad)
            .Include(t => t.Articulo)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TrabajoId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        return await contexto.Trabajos.Include(t => t.TrabajosDetalle)
            .Where(p => p.TrabajoId == id)
            .ExecuteDeleteAsync() > 0;
   
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await contexto.Trabajos
            .Include(t => t.Cliente)
            .Include(t => t.Tecnico)
            .Include(t => t.Prioridad)
            .Include(t => t.Articulo)
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

}
