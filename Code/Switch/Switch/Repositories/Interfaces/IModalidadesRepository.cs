using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IModalidadesRepository
    {
        Task<List<Modalidades>> GetModalidades();

        Task<bool> PostModalidades(Modalidades modalidades);
    }
}
