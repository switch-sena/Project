
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<Usuarios> GetUsuarioById(int id);
        Task<bool> PostUsuario(Usuarios usuario);
        Task<bool> UpdateUsuario(Usuarios usuario);
        Task<bool> DeleteUsuario(int id);
    }
}
