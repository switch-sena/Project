using Switch.Models;

namespace Switch.Repositories.Interfaces
{
    public interface IPublHabiRepository
    {
        Task<List<PublHabi>> GetPublHabi();
        Task<bool> PostPublHabi(PublHabi publHabi);
        Task<bool> UpdatePublHabi(int id, PublHabi publHabi);
        Task<bool> DeletePublHabi(int id);
    }
}
