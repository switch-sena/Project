
using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IPublicacionesRepository
    {
        Task<List<Publicaciones>> GetPublicaciones();
        Task<List<Publicaciones>> GetPublicacionesInfoComp();
        Task<Publicaciones> GetPublicacionesById(int id);
        Task<Publicaciones> GetPublicacionesInfoCompById(int id);
        Task<bool> PostPublicaciones(Publicaciones publicacion);
        Task<bool> UpdatePublicaciones(int id, Publicaciones publicacion);
        Task<bool> DeletePublicaciones(int id);
    }
}
