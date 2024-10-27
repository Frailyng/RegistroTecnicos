using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Cotizaciones
    {
        [Key]
        public int CotizacionId { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo ClienteId es obligatorio")]
        public int ClienteId { get; set; }
        public virtual Clientes? Cliente { get; set; }

        public string Observacion { get; set; }
        public double Monto { get; set; }

       // public ICollection<CotizacionesDetalle> CotizacionesDetalle { get; set; } = new List<CotizacionesDetalle>();
}
}
