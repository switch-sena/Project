
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<List<UsuariosDTO>> GetUsuariosDTO();
        Task<Usuarios> GetUsuariosById(int id);
        Task<UsuariosDTO> GetUsuariosDTOById(int id);
        Task<Usuarios> GetUsuariosByEmail(string email);
        Task<bool> PostUsuarios(Usuarios usuario);
        Task<bool> PostUsuariosDTO(UsuariosDTO usuario);
        Task<bool> UpdateUsuarios(Usuarios usuario);
        Task<bool> UpdateUsuariosDTO(UsuariosDTO usuario);
        Task<bool> DeleteUsuarios(int id);
        Task<bool> DeleteUsuariosDTO(int id);
    }
}
