using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IHabilidadesRepository
    {
        Task<List<Habilidades>> GetHabilidades();
        Task<Habilidades> GetHabilidadesById(int id);
        Task<bool> PostHabilidades(Habilidades habilidad);
        Task<bool> UpdateHabilidades(Habilidades habilidad);
        Task<bool> DeleteHabilidades(int id);
    }
}
