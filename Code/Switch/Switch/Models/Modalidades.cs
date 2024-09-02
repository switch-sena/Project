using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class Modalidades
    {
        [Key]
        public int IdModa { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreModa { get; set; }

        public string DescripcionModa { get; set; }

        // Propiedad de navegación
        public ICollection<PublModa> PublModa { get; set; }
    }
}