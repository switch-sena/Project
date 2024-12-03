using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeMicroservices.Models
{
    public class PublicacionesDTO
    {
        public int IdPubl { get; set; }
        public string TituloPubl { get; set; }
        public string DescripcionPubl { get; set; }
        public string NombreUsuario { get; set; }
        public int CopiaIdUsua { get; set; }
        public List<string> Habilidades { get; set; }
        public List<string> Modalidades { get; set; }
        public List<int> IdHabi { get; set; }
        public List<int> IdModa { get; set; }

    }
}