using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Switch.Models
{
    public class Publicaciones
    {
        [Key]
        public int IdPubl { get; set; }

        public int CopiaIdusua { get; set; }
        public int CopiaIdmoda { get; set; }

        [Required]
        [MaxLength(60)]
        public string TituloPubl { get; set; }

        [Required]
        public string DescripcionPubl { get; set; }

        // Propiedades de navegación
        [ForeignKey("CopiaIdusua")]
        public Usuario Usuario { get; set; }

        [ForeignKey("CopiaIdmoda")]
        public Modalidad Modalidad { get; set; }

        public ICollection<PublHabi> PublHabis { get; set; }
    }
}