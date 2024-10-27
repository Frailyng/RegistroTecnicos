using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionesService(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<bool> Existe(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones.AnyAsync(t => t.CotizacionId == cotizacionId);
    }

    public async Task<bool> Insertar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Cotizaciones.Add(cotizacion);
        await AfectarCotizaciones(cotizacion.CotizacionesDetalle.ToArray());
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task AfectarCotizaciones(CotizacionesDetalle[] detalle)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalle)
        {
            var articulo = await contexto.Articulos.SingleAsync(t => t.ArticuloId == item.ArticuloId);
        }
    }

    public async Task<bool> Modificar(Cotizaciones cotizacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(cotizacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Cotizaciones cotizacion)
    {
        if (!await Existe(cotizacion.CotizacionId))
        {
            return await Insertar(cotizacion);
        }
        else
        {
            return await Modificar(cotizacion);
        }
    }

    public async Task<Cotizaciones> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.CotizacionesDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.CotizacionId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.CotizacionesDetalle)
            .Where(t => t.CotizacionId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(t => t.Cliente)
            .Include(t => t.CotizacionesDetalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
