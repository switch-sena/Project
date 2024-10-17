using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IModalidadesRepository
    {
        Task<List<Modalidades>> GetModalidades();
        Task<Modalidades> GetModalidadesById(int id);
        Task<bool> PostModalidades(Modalidades modalidades);
        Task<bool> UpdateModalidades(Modalidades modalidades);
        Task<bool> DeleteModalidades(int id);
    }
}
