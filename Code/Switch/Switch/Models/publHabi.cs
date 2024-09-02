﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Switch.Models
{
    public class PublHabi
    {
        [Key]
        public int Id { get; set; }

        public int CopiaIdPubl { get; set; }
        public int CopiaIdHabi { get; set; }

        // Propiedades de navegación
        [ForeignKey("CopiaIdPubl")]
        public Publicaciones Publicaciones { get; set; }

        [ForeignKey("CopiaIdHabi")]
        public Habilidades Habilidades { get; set; }
    }
}
