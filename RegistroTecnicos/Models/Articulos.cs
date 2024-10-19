using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Articulos
{
    [Key]

    public int ArticuloId { get; set; }

    [Required(ErrorMessage = "La Descripcion no puede Estar en blanco")]

    public string Descripcion { get; set; }

    public double Costo { get; set; }

    public double Precio { get; set; }

    public int Existencia { get; set; }
}
