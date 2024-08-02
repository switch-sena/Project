using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Switch.Models
{
    public class Habilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Habi { get; set; }

        [Required]
        [DisplayName("Nombre de la habilidad")]
        [StringLength(50, ErrorMessage = "No excederse de 50 caracteres")]
        public string Nombre_Habi { get; set; }

        [Required]
        [DisplayName("Descripcion de la habilidad")]
        public string Descripcion_Habi { get; set; }
    }
}