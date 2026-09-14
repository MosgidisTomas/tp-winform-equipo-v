using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
        public class AccesoDatos
        {
            private SqlConnection conexion;
            private SqlCommand comando;
            private SqlDataReader lector;

            public SqlDataReader Lector
            {
                get { return lector; }
            }

            public AccesoDatos()
            {
                conexion = new SqlConnection();
                comando = new SqlCommand();

                try
                {
                    try
                    {
                        conexion.ConnectionString = "server=localhost; database=CATALOGO_P3_DB; user id=sa; password=Mateo.123;";
                        conexion.Open();
                    }
                    catch
                    {
                        conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true;";
                        conexion.Open();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
            public void setearConsulta(string consulta)
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;
            }

            public void ejecutarLectura()
            {
                comando.Connection = conexion;
                try
                {
                    lector = comando.ExecuteReader();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public void setearParametro(string nombre, object valor)
            {
                comando.Parameters.AddWithValue(nombre, valor);
            }

            public void ejecutarAccion()
            {
                comando.Connection = conexion;
                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void cerrarConexion()
                    {
                        if (lector != null && !lector.IsClosed)
                            lector.Close();
                        conexion.Close();
                    }

            }  
}
