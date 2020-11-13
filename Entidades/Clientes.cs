using System;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
    }
}