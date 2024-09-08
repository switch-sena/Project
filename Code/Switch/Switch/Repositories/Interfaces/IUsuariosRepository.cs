
using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<Usuarios> GetUsuarioById(int id);
        Task<bool> CreateUsuario(Usuarios usuario);
        Task<bool> UpdateUsuario(Usuarios usuario);
        Task<bool> DeleteUsuario(int id);
    }
}
