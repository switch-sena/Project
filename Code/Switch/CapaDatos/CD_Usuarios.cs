using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<usuario> Listar()
        {
            List<usuario> lista = new List<usuario>();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    string query = "SELECT `id_usua`, `nombre_usua`, `apellido_usua`, `fecha_nacimiento_usua`, `celular_usua`, `correo_usua` FROM `usuario`";
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) 
                        {
                            lista.Add
                                (
                                    new usuario()
                                    {
                                        id_usua = Convert.ToInt32(dr["id_usua"]),
                                        nombre_usua = dr["nombre_usua"].ToString(),
                                        apellido_usua = dr["apellido_usua"].ToString(),
                                        fecha_nacimiento_usua = Convert.ToDateTime(dr["fecha_nacimiento_usua"]),
                                        celular_usua = dr["celular_usua"].ToString(),
                                        correo_usua = dr["correo_usua"].ToString()
                                    }
                                );
                        }

                    }
                }
            }
            catch
            {
                lista = new List<usuario>();
            }

            return lista;
        }
    }
}
