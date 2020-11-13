using System;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public string Suplidor { get; set; }
        public double Precio { get; set; }
        public double Existencia { get; set; }

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("SuplidorId")]
        public Suplidores suplidores { get; set; }
    }
}
