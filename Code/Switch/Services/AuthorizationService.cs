using Switch.Context;
using Switch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Switch.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly SwitchContext db = new SwitchContext();

        public AuthResults Auth(string correo, string password, out Usuario usuario)
        {
            var users = db.Usuarios.ToList();
            usuario = db.Usuarios.SingleOrDefault(p => p.CorreoUsua == correo);
            if (usuario == null)
                return AuthResults.NotExists;

            if (password != usuario.ClaveUsua)
                return AuthResults.PasswordNotMatch;

            return AuthResults.Success;
        }
    }
}