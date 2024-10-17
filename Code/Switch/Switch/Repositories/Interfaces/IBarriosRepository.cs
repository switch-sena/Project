using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IBarriosRepository
    {
        Task<List<Barrios>> GetBarrios();
        Task<Barrios> GetBarriosById(int id);
        Task<bool> PostBarrios(Barrios barrios);
        Task<bool> UpdateBarrios(Barrios barrios);
        Task<bool> DeleteBarrios(int id);

    }
}
