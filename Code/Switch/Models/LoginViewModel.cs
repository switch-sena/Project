using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Switch.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Correo { get; set; }

        [PasswordPropertyText]
        [Required]
        public string Clave { get; set; }
    }
}