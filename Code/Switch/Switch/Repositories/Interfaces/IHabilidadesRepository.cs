using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IHabilidadesRepository
    {
        Task<List<Habilidades>> GetHabilidades();
        Task<Habilidades> GetHabilidadById(int id);
        Task<bool> PostHabilidad(Habilidades habilidad);
        Task<bool> UpdateHabilidad(Habilidades habilidad);
        Task<bool> DeleteHabilidad(int id);
    }
}
