using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.DAL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.BLL
{
    public class FacturacionBLL
    {
        //—————————————————————————————————————————————————————[ GUARDAR ]—————————————————————————————————————————————————————
        public static bool Guardar(Facturacion facturacion)
        {
            bool paso;

            if (!Existe(facturacion.FacturacionId))
                paso = Insertar(facturacion);
            else
                paso = Modificar(facturacion);

            return paso;
        }
        //—————————————————————————————————————————————————————[ INSERTAR ]—————————————————————————————————————————————————————
        public static bool Insertar(Facturacion facturacion)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                foreach (var item in facturacion.Detalle)
                {
                    contexto.Entry(item.productos).State = EntityState.Modified;
                }

                contexto.Facturacion.Add(facturacion);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //—————————————————————————————————————————————————————[ MODIFICAR ]—————————————————————————————————————————————————————
        public static bool Modificar(Facturacion facturacion)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete From FacturacionDetalle Where FacturacionId={facturacion.FacturacionId}");

                foreach (var item in facturacion.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(facturacion).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //—————————————————————————————————————————————————————[ ELIMINAR ]—————————————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var facturacion = contexto.Facturacion.Find(id);
                if (facturacion != null)
                {
                    contexto.Facturacion.Remove(facturacion);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        //—————————————————————————————————————————————————————[ GETLIST ]—————————————————————————————————————————————————————
        public static List<Facturacion> GetList(Expression<Func<Facturacion, bool>> criterio)
        {
            List<Facturacion> lista = new List<Facturacion>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Facturacion.Where(criterio).ToList();
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
        //—————————————————————————————————————————————————————[ EXISTE ]—————————————————————————————————————————————————————
        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Facturacion.Any(p => p.FacturacionId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
        //—————————————————————————————————————————————————————[ BUSCAR ]————————————————————————————————————————————————————
        public static Facturacion Buscar(int id)
        {
            Facturacion facturacion = new Facturacion();
            Contexto contexto = new Contexto();

            try
            {
                facturacion = contexto.Facturacion
                    .Where(p => p.FacturacionId == id)
                    .Include(p => p.Detalle)
                    .ThenInclude(t => t.productos)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return facturacion;
        }
    }
}