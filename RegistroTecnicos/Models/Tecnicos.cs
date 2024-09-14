using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "El Campo Nombres es obligatorio")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio Seleccione una de las opciones Disponibles")]
        public string? Descripcion { get; set; }

        public int SueldoHora { get; set; }

        // Relación con Trabajos
        public virtual ICollection<Trabajos> Trabajos { get; set; } = new List<Trabajos>();
    }
}
