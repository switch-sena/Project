using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IBarriosRepository
    {
        Task<List<Barrios>> GetBarrios();

        Task<bool> PostBarrios(Barrios barrios);
    }
}
