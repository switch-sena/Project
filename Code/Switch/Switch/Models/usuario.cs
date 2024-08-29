using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Switch.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public string ApellidoUsua { get; set; }

        [Required]
        [MaxLength(10)]
        public string GeneroUsua { get; set; }

        [Required]
        public DateTime FechaNacimientoUsua { get; set; }

        [Required]
        [MaxLength(10)]
        public string CelularUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public string CorreoUsua { get; set; }

        [Required]
        public string ClaveUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public string CorreoElectronicoUsua { get; set; }

        public string LinksRsUsua { get; set; }

        public int CopiaIdbarr { get; set; }

        public DateTime CreacionfechaUsua { get; set; } = DateTime.Now;
        public DateTime? ModificacionfechaUsua { get; set; }

        // Propiedad de navegación
        [ForeignKey("CopiaIdbarr")]
        public Barrio Barrio { get; set; }

        public ICollection<Publicacion> Publicaciones { get; set; }
    }
}