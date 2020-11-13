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
    public class EntradaProductosBLL
    {
        //——————————————————————————————————————————————[ GUARDAR ]——————————————————————————————————————————————
        public static bool Guardar(EntradaProductos entradaProductos)
        {
            if (!Existe(entradaProductos.ProductoId))
                return Insertar(entradaProductos);
            else
                return Modificar(entradaProductos);
        }
        //——————————————————————————————————————————————[ INSERTAR ]——————————————————————————————————————————————
        private static bool Insertar(EntradaProductos entradaProductos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.EntradaProductos.Add(entradaProductos);
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
        public static bool Modificar(EntradaProductos entradaProductos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(entradaProductos).State = EntityState.Modified;
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
                var entradaProductos = contexto.EntradaProductos.Find(id);
                if (entradaProductos != null)
                {
                    contexto.EntradaProductos.Remove(entradaProductos);
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
        public static EntradaProductos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            EntradaProductos entradaProductos;

            try
            {
                entradaProductos = contexto.EntradaProductos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entradaProductos;
        }
        //——————————————————————————————————————————————[ GETLIST ]——————————————————————————————————————————————
        public static List<EntradaProductos> GetList(Expression<Func<EntradaProductos, bool>> criterio)
        {
            List<EntradaProductos> lista = new List<EntradaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradaProductos.Where(criterio).ToList();
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
                encontrado = contexto.EntradaProductos.Any(c => c.ProductoId == id);
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
        public static List<EntradaProductos> GetList()
        {
            List<EntradaProductos> entradaProductos = new List<EntradaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                entradaProductos = contexto.EntradaProductos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entradaProductos;
        }
        //——————————————————————————————————————————————[ GET ]——————————————————————————————————————————————
        public static List<EntradaProductos> GetProductos()
        {
            List<EntradaProductos> lista = new List<EntradaProductos>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.EntradaProductos.ToList();
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