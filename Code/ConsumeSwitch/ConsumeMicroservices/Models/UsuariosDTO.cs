using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeMicroservices.Models
{
    public class UsuariosDTO
    {
        public int IdUsua { get; set; }
        public string NombreUsua { get; set; }
        public string ApellidoUsua { get; set; }
        public string GeneroUsua { get; set; }
        public DateTime FechaNacimientoUsua { get; set; }
        public string CelularUsua { get; set; }
        public string CorreoUsua { get; set; }
        public string ClaveUsua { get; set; }
        public string CorreoElectronicoUsua { get; set; }
        public string LinksRsUsua { get; set; }
        public int IdBarr { get; set; }
    }
}