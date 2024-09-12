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

    private 
}
