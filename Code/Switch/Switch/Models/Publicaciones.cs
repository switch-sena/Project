

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Usuarios? Usuarios { get; set; }

        // Propiedad de navegación
        [JsonIgnore]
        public ICollection<PublModa>? PublModa { get; set; }
    }
}
