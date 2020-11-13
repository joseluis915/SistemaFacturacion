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
    public class SalidaProductosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(SalidaProductos salidaProductos)
        {
            if (!Existe(salidaProductos.ProductoId))
                return Insertar(salidaProductos);
            else
                return Modificar(salidaProductos);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(SalidaProductos salidaProductos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.SalidaProductos.Add(salidaProductos);
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
        //——————————————————————————————————————————————[ MODIFICAR ]——————————————————————————————————————————————
        public static bool Modificar(SalidaProductos salidaProductos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(salidaProductos).State = EntityState.Modified;
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
        //——————————————————————————————————————————————[ ELIMINAR ]——————————————————————————————————————————————
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var salidaProductos = contexto.SalidaProductos.Find(id);
                if (salidaProductos != null)
                {
                    contexto.SalidaProductos.Remove(salidaProductos);
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
        //——————————————————————————————————————————————[ BUSCAR ]——————————————————————————————————————————————
        public static SalidaProductos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            SalidaProductos salidaProductos;

            try
            {
                salidaProductos = contexto.SalidaProductos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return salidaProductos;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<SalidaProductos> GetList(Expression<Func<SalidaProductos, bool>> criterio)
        {
            List<SalidaProductos> lista = new List<SalidaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.SalidaProductos.Where(criterio).ToList();
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
        //——————————————————————————————————————————————[ EXISTE ]——————————————————————————————————————————————
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.SalidaProductos.Any(c => c.ProductoId == id);
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
        //——————————————————————————————————————————————[ GetList ]——————————————————————————————————————————————
        public static List<SalidaProductos> GetList()
        {
            List<SalidaProductos> salidaProductos = new List<SalidaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                salidaProductos = contexto.SalidaProductos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return salidaProductos;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<SalidaProductos> GetProductos()
        {
            List<SalidaProductos> lista = new List<SalidaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.SalidaProductos.ToList();
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