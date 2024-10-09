﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsumeSwitch.Models
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
        public ICollection<Usuarios>? Usuarios { get; set; }
    }
}