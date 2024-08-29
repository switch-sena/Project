using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class PublHabi
    {
        [Key]
        public int Id { get; set; }

        public int? CopiaIdpubl { get; set; }
        public int? CopiaIdhabi { get; set; }

        // Propiedades de navegación
        [ForeignKey("CopiaIdpubl")]
        public Publicaciones Publicacion { get; set; }

        [ForeignKey("CopiaIdhabi")]
        public Habilidad Habilidad { get; set; }
    }
}