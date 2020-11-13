using System;
using System.Collections.Generic;
//Using agregados
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using SistemaFacturacion.DAL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.BLL
{
    public class ClientesBLL
    {
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Clientes> GetClientes()
        {
            List<Clientes> lista = new List<Clientes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Clientes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}