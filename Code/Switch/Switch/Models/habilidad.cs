using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class Habilidad
    {
        [Key]
        public int IdHabi { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreHabi { get; set; }

        public string DescripcionHabi { get; set; }

        // Propiedad de navegación
        public ICollection<PublHabi> PublHabis { get; set; }
    }
}