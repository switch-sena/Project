
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IPublicacionesRepository
    {
        Task<List<Publicaciones>> GetPublicaciones();
        Task<List<PublicacionesDTO>> GetPublicacionesInfoComp();
        Task<Publicaciones> GetPublicacionesById(int id);
        Task<PublicacionesDTO> GetPublicacionesInfoCompByPubl(int id);
        Task<List<PublicacionesDTO>> GetPublicacionesInfoCompByUsuario(int id);
        Task<bool> PostPublicacionesDTO(PublicacionesDTO publicacionDTO);
        Task<bool> PostPublicaciones(Publicaciones publicacion);
        Task<bool> UpdatePublicaciones(int id, Publicaciones publicacion);
        Task<bool> UpdatePublicacionesDTO(PublicacionesDTO publicaciones);
        Task<bool> DeletePublicaciones(int id);
        Task<bool> DeletePublicacionesDTO(int id);

    }
}
