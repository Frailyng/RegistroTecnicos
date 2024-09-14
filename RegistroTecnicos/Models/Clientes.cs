using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "El campo Descripcion es obligatorio Seleccione una de las opciones Disponibles")]
    public string? WhatsApp { get; set; }

    // Relación con Trabajos
    public virtual ICollection<Trabajos> Trabajos { get; set; } = new List<Trabajos>();
}
