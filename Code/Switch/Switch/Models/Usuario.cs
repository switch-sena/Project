using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Switch.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public Datetime Fecha_Nacimiento { get; set; }
        public string Celular { get; set; }
        public string Correo_Electronico { get; set; }
        public string Links_RS { get; set; }
        public string Habilidades { get; set; }
    }
}