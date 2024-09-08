

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class Publicaciones
    {
        [Key]
        public int IdPubl { get; set; }

        [Required]
        public string TituloPubl { get; set; }

        public string DescripcionPubl { get; set; }

        public int CopiaIdUsua { get; set; }

        // Relación con Usuarios
        public Usuarios Usuarios { get; set; }

        // Propiedad de navegación
        public ICollection<PublModa> PublModa { get; set; }
        public object PublHabi { get; set; }
    }
}
