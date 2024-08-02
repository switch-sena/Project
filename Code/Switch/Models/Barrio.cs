using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Switch.Models
{
    public class Barrio
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBarr { get; set; }

        [Required]
        [DisplayName("Nombre del Barrio")]
        public string NombreBarr { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}