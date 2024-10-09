using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IModalidadesRepository
    {
        Task<List<Modalidades>> GetModalidades();

        Task<bool> PostModalidades(Modalidades modalidades);
    }
}
