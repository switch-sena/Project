using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class Barrios
    {
        [Key]
        public int IdBarr { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreBarr { get; set; }

        // Propiedad de navegación
        public ICollection<Usuario> Usuarios { get; set; }
    }
}