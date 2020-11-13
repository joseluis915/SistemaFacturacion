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
    public class SuplidoresBLL
    {
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<Suplidores> GetSuplidores()
        {
            List<Suplidores> lista = new List<Suplidores>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Suplidores.ToList();
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