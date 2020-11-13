using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades
{
    public class Facturacion
    {
        [Key]
        public int FacturacionId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public double Total { get; set; }

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("FacturacionId")]
        public virtual List<FacturacionDetalle> Detalle { get; set; } = new List<FacturacionDetalle>();
    }
}