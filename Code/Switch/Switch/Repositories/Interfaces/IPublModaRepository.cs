using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IPublModaRepository
    {
        Task<List<PublModa>> GetPublModa();
        Task<bool> PostPublModa(PublModa publModa);
        Task<bool> UpdatePublModa(int id, PublModa publModa);
        Task<bool> DeletePublModa(int id);
    }
}
