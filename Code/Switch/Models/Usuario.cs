using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Switch.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsua { get; set; }

        [Required]
        [DisplayName("Nombres")]
        public string NombreUsua { get; set; }

        [Required]
        [DisplayName("Apellidos")]
        public string ApellidoUsua { get; set; }

        [Required]
        [DisplayName("Genero")]
        public string GeneroUsua { get; set; }

        [Required]
        [DisplayName("Fecha de Nacimiento")]
        public DateTime? FechaNacimientoUsua { get; set; }

        [Required]
        [DisplayName("Celular")]
        [StringLength(10, ErrorMessage = "No se permiten mas de 10 dígitos")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Solo números")]
        public string CelularUsua { get; set; }

        [Required]
        [DisplayName("Usuario")]
        public string CorreoUsua { get; set; }

        [Required]
        [DisplayName("Clave")]
        [DataType(DataType.Password)]
        public string ClaveUsua { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite un email válido")]
        public string CorreoElectronicoUsua { get; set; }

        [Required]
        [DisplayName("Redes Sociales")]
        public string? LinksRsUsua { get; set; }

        [Required]
        [DisplayName("Barrio")]
        public int CopiaIdBarr { get; set; }

        [ForeignKey("CopiaIdBarr")]
        public virtual Barrio Barrio { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreacionFechaUsua { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModificacionFechaUsua { get; set; }
    }
}