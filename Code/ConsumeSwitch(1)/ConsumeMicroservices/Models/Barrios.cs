using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ConsumeMicroservices.Models
{
    public class Barrios
    {
        [Key]
        public int IdBarr { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreBarr { get; set; }

        // Propiedad de navegación
        [JsonIgnore]
        // preguntar al profe porque produce error con el "?"
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}