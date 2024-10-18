using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Text.Json.Serialization;

namespace SwitchBack.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsua { get; set; }

        [Required]
        [MaxLength(100)]
       // [JsonIgnore] para swager---evitar llenar campos
        public required string NombreUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ApellidoUsua { get; set; }

        [Required]
        [MaxLength(10)]
        public required string GeneroUsua { get; set; }

        [Required]
        public DateTime FechaNacimientoUsua { get; set; }

        [Required]
        [MaxLength(10)]
        public required string CelularUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public required string CorreoUsua { get; set; }

        [Required]
        public required string ClaveUsua { get; set; }

        [Required]
        [MaxLength(100)]
        public required string CorreoElectronicoUsua { get; set; }

        public string? LinksRsUsua { get; set; }

        public int CopiaIdBarr { get; set; }

        public DateTime CreacionFechaUsua { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? ModificacionFechaUsua { get; set; }

        // Propiedad de navegación
        [JsonIgnore]
        [ForeignKey("CopiaIdBarr")]
        public Barrios? Barrio { get; set; }

        [JsonIgnore]
        public ICollection<Publicaciones>? Publicaciones { get; set; }
    }
}

