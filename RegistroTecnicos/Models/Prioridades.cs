using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Prioridades
{
    [Key]

    public int PrioridadId { get; set; }

    [Required(ErrorMessage = "El campo Descripcion es obligatorio")]

    public string? Descripcion { get; set; }

    public string? Tiempo { get; set; }
}
