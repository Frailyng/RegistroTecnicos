using System.ComponentModel.DataAnnotations;
namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "El Campo Nombres es obligatorio")]
    public string? Descripcion {  get; set; }
    public int DiasCompromiso { get; set; }
}
