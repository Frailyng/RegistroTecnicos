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
}
