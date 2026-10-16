using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Descripcion"] is DBNull)) aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                datos.cerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public bool existe(string descripcion, int idExcluir = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM CATEGORIAS WHERE LOWER(LTRIM(RTRIM(Descripcion))) = LOWER(@Descripcion) AND Id <> @Id");
                datos.setearParametro("@Descripcion", descripcion.Trim());
                datos.setearParametro("@Id", idExcluir);
                object resultado = datos.ejecutarAccionEscalar();
                return Convert.ToInt32(resultado) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool agregar(Categoria nueva)
        {
            if (existe(nueva.Descripcion))
                return false;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@Descripcion)");
                datos.setearParametro("@Descripcion", nueva.Descripcion.Trim());
                datos.ejecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Descripcion = @Descripcion WHERE Id = @Id");
                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Id", categoria.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdCategoria = @Id");
                datos.setearParametro("@Id", id);
                int cantidadArticulos = Convert.ToInt32(datos.ejecutarAccionEscalar());

                if (cantidadArticulos > 0)
                    throw new Exception("No se puede eliminar la categoria: tiene " + cantidadArticulos + " articulo(s) asociado(s).");

                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @Id");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
