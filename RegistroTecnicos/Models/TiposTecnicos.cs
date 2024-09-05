using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoTecnicoId { get; set; }
    [Required(ErrorMessage = "El Campo Descripcion es obligatorio")]
    public string? Descripcion { get; set; }

    public bool? Activo {  get; set; }
}
