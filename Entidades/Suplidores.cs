using System;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Entidades
{
    public class Suplidores
    {
        [Key]
        public int SuplidorId { get; set; }
        public string NombreComercial { get; set; }
    }
}