using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public ICollection<PublModa>? PublModa { get; set; }
    }
}