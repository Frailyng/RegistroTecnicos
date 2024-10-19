using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public partial class TrabajosDetalle
{
    [Key]
    public int DetalleId { get; set; }

    public int TrabajoId { get; set; }

    public int ArticuloId { get; set; }

    public int Cantidad { get; set; }

    public double Precio { get; set; }

    public double Costo { get; set; }

    [ForeignKey("TrabajoId")]
    public virtual Trabajos Trabajo { get; set; } = null!;

}
