using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IBarriosRepository
    {
        Task<List<Barrios>> GetBarrios();

        Task<bool> PostBarrios(Barrios barrios);
    }
}
