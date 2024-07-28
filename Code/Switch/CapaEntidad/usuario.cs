using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class usuario
    {
        public int id_usua { get; set; }
        public string nombre_usua { get; set; }
        public string apellido_usua { get; set; }
        public string genero_usua { get; set; }
        public string celular_usua { get; set; }
        public string correo_usua { get; set; }
        public string clave_usua { get; set; }
        public string correo_electronico_usua { get; set; }
        public DateTime fecha_nacimiento_usua { get; set; }
        public string link_rs_usua { get; set; }
        public barrios obarrios { get; set; }
    }
}
