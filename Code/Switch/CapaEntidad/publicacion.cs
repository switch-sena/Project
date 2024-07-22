using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class publicacion
    {
        public int id_publ { get; set; }
        public usuario ousuario { get; set; }
        public modalidad omodalidad { get; set; }
        public string titulo_publ { get; set; }
        public string descripcion_publ { get; set; }
    }
}
