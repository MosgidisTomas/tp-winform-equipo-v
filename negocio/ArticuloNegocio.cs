using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion AS Marca, A.IdCategoria, C.Descripcion AS Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull)) aux.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull)) aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull)) aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (datos.Lector["Marca"] is DBNull)
                            ? "Marca no encontrada"
                            : (string)datos.Lector["Marca"];
                    }
                    else
                    {
                        aux.Marca.Id = 0;
                        aux.Marca.Descripcion = "Sin marca";
                    }

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (datos.Lector["Categoria"] is DBNull)
                            ? "Categoria no encontrada"
                            : (string)datos.Lector["Categoria"];
                    }
                    else
                    {
                        aux.Categoria.Id = 0;
                        aux.Categoria.Descripcion = "Sin categoria";
                    }

                    if (!(datos.Lector["Precio"] is DBNull)) aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                datos.cerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (Articulo aux in lista)
            {
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                aux.Imagenes = imagenNegocio.listar(aux.Id);
            }

            return lista;
        }

        public int agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);
                object resultado = datos.ejecutarAccionEscalar();
                return Convert.ToInt32(resultado);
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

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio WHERE Id = @Id");
                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Id", articulo.Id);
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
        public List <Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List <Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion AS Marca, A.IdCategoria, C.Descripcion AS Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id WHERE ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > @filtro";
                            break;
                        case "Menor a":
                            consulta += "A.Precio < @filtro";
                            break;
                        default:
                            consulta += "A.Precio = @filtro";
                            break;
                    }

                    datos.setearConsulta(consulta);
                    datos.setearParametro("@filtro", decimal.Parse(filtro));
                }
                else
                {
                    string columna;
                    if (campo == "Codigo") columna = "A.Codigo";
                    else if (campo == "Nombre") columna = "A.Nombre";
                    else if (campo == "Marca") columna = "M.Descripcion";
                    else columna = "C.Descripcion";

                    consulta += columna + " LIKE @filtro";
                    datos.setearConsulta(consulta);

                    switch (criterio)
                    {
                        case "Comienza con":
                            datos.setearParametro("@filtro", filtro + "%");
                            break;
                        case "Termina con":
                            datos.setearParametro("@filtro", "%" + filtro);
                            break;
                        default:
                            datos.setearParametro("@filtro", "%" + filtro + "%");
                            break;
                    }
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull)) aux.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull)) aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull)) aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (datos.Lector["Marca"] is DBNull)
                            ? "Marca no encontrada"
                            : (string)datos.Lector["Marca"];
                    }
                    else
                    {
                        aux.Marca.Id = 0;
                        aux.Marca.Descripcion = "Sin marca";
                    }

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (datos.Lector["Categoria"] is DBNull)
                            ? "Categoria no encontrada"
                            : (string)datos.Lector["Categoria"];
                    }
                    else
                    {
                        aux.Categoria.Id = 0;
                        aux.Categoria.Descripcion = "Sin categoria";
                    }

                    if (!(datos.Lector["Precio"] is DBNull)) aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                datos.cerrarConexion();

                foreach (Articulo aux in lista)
                {
                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    aux.Imagenes = imagenNegocio.listar(aux.Id);
                }

                return lista;
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
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            imagenNegocio.eliminarPorArticulo(id);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id");
                datos.setearParametro("@Id", id);
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