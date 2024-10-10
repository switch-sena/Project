using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsumeMicroservices.Models
{
    public class PublModa
    {
        [Key]
        public int Id { get; set; }

        public int CopiaIdPubl { get; set; }

        public int CopiaIdModa { get; set; }

        [JsonIgnore]
        [ForeignKey("CopiaIdPubl")]
        public Publicaciones? Publicaciones { get; set; }

        [JsonIgnore]
        [ForeignKey("CopiaIdModa")]
        public Modalidades? Modalidades { get; set; }
    }
}
