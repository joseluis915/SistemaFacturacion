using System;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Entidades
{
    public class SalidaProductos
    {
        [Key]
        public int SalidaProductoId { get; set; }
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public DateTime FechaSalida { get; set; } = DateTime.Now;

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("ProductoId")]
        public Productos productos { get; set; }
    }
}