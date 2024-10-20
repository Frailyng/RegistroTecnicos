using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public partial class ArticulosService(Contexto contexto)
{
    public async Task <List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        return await contexto.Articulos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Articulos> BuscarPorId(int articuloId)
    {
        return await contexto.Articulos.FirstOrDefaultAsync(a => a.ArticuloId == articuloId);
    }

    public async Task<bool> Actualizar(Articulos articulo)
    {
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
