using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SistemaFacturacion.Entidades
{
    public class FacturacionDetalle
    {
        [Key]
        public int FacturacionDetalleId { get; set; }
        public int FacturacionId { get; set; }
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public double SubTotal { get; set; }

        //———————————————————————————[ ForeingKeys ]———————————————————————————
        [ForeignKey("ProductoId")]
        public Productos productos { get; set; } = new Productos();
    }
}