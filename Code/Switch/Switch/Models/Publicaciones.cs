using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Switch.Models
{
    public class Publicaciones
    {
        [Key]
        public int IdPubl { get; set; }

        public int CopiaIdUsua { get; set; }

        [Required]
        [MaxLength(60)]
        public string TituloPubl { get; set; }

        [Required]
        public string DescripcionPubl { get; set; }

        // Propiedades de navegación
        [ForeignKey("CopiaIdUsua")]
        public Usuarios Usuarios { get; set; }

        public ICollection<PublHabi> PublHabi { get; set; }
        public ICollection<PublModa> PublModa { get; set; }

    }
}