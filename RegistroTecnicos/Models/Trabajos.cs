﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Trabajos
    {
        [Key]
        public int TrabajoId { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo ClienteId es obligatorio")]
        public int ClienteId { get; set; }
        public virtual Clientes? Cliente { get; set; }

        [Required(ErrorMessage = "El campo TecnicoId es obligatorio")]
        public int TecnicoId { get; set; }
        public virtual Tecnicos? Tecnico { get; set; }

        public string Descripcion { get; set; }
        public double Total { get; set; }

        [Required(ErrorMessage = "El campo Prioridad es obligatorio")]
        public int PrioridadId { get; set; } 
        public virtual Prioridades? Prioridad { get; set; } 

        public ICollection<TrabajosDetalle> TrabajosDetalle {  get; set; } = new List<TrabajosDetalle>();
    }
}
