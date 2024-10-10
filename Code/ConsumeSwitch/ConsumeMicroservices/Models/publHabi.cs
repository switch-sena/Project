using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsumeMicroservices.Models
{
    public class PublHabi
    {
        [Key]
        public int Id { get; set; }

        public int CopiaIdPubl { get; set; }
        public int CopiaIdHabi { get; set; }

        // Propiedades de navegación
        [JsonIgnore]
        [ForeignKey("CopiaIdPubl")]
        public Publicaciones? Publicaciones { get; set; }

        [JsonIgnore]
        [ForeignKey("CopiaIdHabi")]
        public Habilidades? Habilidades { get; set; }
    }
}
