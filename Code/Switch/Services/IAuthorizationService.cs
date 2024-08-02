using Switch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Services
{
    public interface IAuthorizationService
    {
        AuthResults Auth(string user, string password, out Usuario usuario);
    }

    public enum AuthResults
    {
        Success,
        PasswordNotMatch,
        NotExists,
        Error
    }
}
