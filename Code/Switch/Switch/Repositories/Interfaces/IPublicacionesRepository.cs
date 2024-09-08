
using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IPublicacionesRepository
    {
        Task<List<Publicaciones>> GetPublicaciones();
        Task<Publicaciones> GetPublicacionById(int id);
        Task<bool> PostPublicacion(Publicaciones publicacion);
        Task<bool> PutPublicacion(int id, Publicaciones publicacion);
        Task<bool> DeletePublicacion(int id);
    }
}
