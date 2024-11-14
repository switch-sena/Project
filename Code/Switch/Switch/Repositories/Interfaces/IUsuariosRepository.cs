
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<Usuarios> GetUsuariosById(int id);
        Task<Usuarios> GetUsuariosByEmail(string email);
        Task<bool> PostUsuarios(Usuarios usuario);
        Task<bool> UpdateUsuarios(Usuarios usuario);
        Task<bool> DeleteUsuarios(int id);
    }
}
