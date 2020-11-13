using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<EntradaProductos> EntradaProductos { get; set; }
        public DbSet<SalidaProductos> SalidaProductos { get; set; }
        public DbSet<Facturacion> Facturacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Data\SistemaFacturacion.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores { SuplidorId = 1, NombreComercial = "Supermercado Yoma" });
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores { SuplidorId = 2, NombreComercial = "Grupo Rica" });
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores { SuplidorId = 3, NombreComercial = "Surtidora Jose Luis" });
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores { SuplidorId = 4, NombreComercial = "Omega Tech" });
            modelBuilder.Entity<Suplidores>().HasData(new Suplidores { SuplidorId = 5, NombreComercial = "NVIDIA" });

            modelBuilder.Entity<Clientes>().HasData(new Clientes { ClienteId = 1, NombreCompleto = "Jose Luis Burgos Hernandez", Cedula = "402-0000000-0", Telefono = "809-000-0000" });
            modelBuilder.Entity<Clientes>().HasData(new Clientes { ClienteId = 2, NombreCompleto = "asd asd asd asd", Cedula = "402-0000000-0", Telefono = "809-000-0000" });
        }
    }
}