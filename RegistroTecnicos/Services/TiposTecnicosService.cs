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
}
