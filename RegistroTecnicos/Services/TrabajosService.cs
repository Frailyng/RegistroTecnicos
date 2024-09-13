using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.Services;

public class TrabajosService
{
    private readonly Contexto _context;


    public async Task<bool> Guardar(Trabajos Trabajos)
    {
        if (!await Existe(Trabajos.TrabajoId))
            return await Insertar(Trabajos);
        else
            return await Modificar(Trabajos);
    }

    private async Task<bool> Insertar(Trabajos Trabajos)
    {
        _context.Trabajos
        return await _context.SaveChangesAsync() > 0;

    }

    private async Task <bool> Modificar(Trabajos Trabajos)
    {
        _context.Update(Trabajos);
    }
}
