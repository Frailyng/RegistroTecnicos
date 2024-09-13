using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }
    [Required(ErrorMessage = "El campo Fecha Es obligatorio")]
    public DateTime? Fecha {  get; set; }
    [Required(ErrorMessage = "El campo ClienteId Es obligatorio")]
    public int ClienteId { get; set; }
    public int TecnicoId { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Monto { get; set; }

}
