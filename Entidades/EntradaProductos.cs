using System;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Entidades
{
    public class EntradaProductos
    {
        [Key]
        public int EntradaProductoId { get; set; }
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; } = DateTime.Now;

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("ProductoId")]
        public Productos productos { get; set; }
    }
}