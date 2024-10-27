using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public partial class ArticulosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task <List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Articulos> BuscarPorId(int articuloId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Articulos.FirstOrDefaultAsync(a => a.ArticuloId == articuloId);
    }

    public async Task<bool> Actualizar(Articulos articulo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        try
        {
            contexto.Articulos.Update(articulo);
            await contexto.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
