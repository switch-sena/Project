
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<List<Usuarios>> GetUsuariosDTO();
        Task<Usuarios> GetUsuariosById(int id);
        Task<Usuarios> GetUsuariosDTOById(int id);
        Task<Usuarios> GetUsuariosByEmail(string email);
        Task<bool> PostUsuarios(Usuarios usuario);
        Task<bool> PostUsuariosDTO(Usuarios usuario);
        Task<bool> UpdateUsuarios(Usuarios usuario);
        Task<bool> UpdateUsuariosDTO(Usuarios usuario);
        Task<bool> DeleteUsuarios(int id);
        Task<bool> DeleteUsuariosDTO(int id);
    }
}
