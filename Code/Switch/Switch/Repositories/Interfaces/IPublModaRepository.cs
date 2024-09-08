using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IPublModaRepository
    {
        Task<List<PublModa>> GetPublModa();
        Task<bool> PostPublModa(PublModa publModa);
        Task<bool> UpdatePublModa(int id, PublModa publModa);
        Task<bool> DeletePublModa(int id);
    }
}
