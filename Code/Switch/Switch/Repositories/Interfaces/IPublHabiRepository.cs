using SwitchBack.Models;

namespace SwitchBack.Repositories.Interfaces
{
    public interface IPublHabiRepository
    {
        Task<List<PublHabi>> GetPublHabi();
        Task<PublHabi> GetPublHabiById(int id);
        Task<bool> PostPublHabi(PublHabi publHabi);
        Task<bool> UpdatePublHabi(int id, PublHabi publHabi);
        Task<bool> DeletePublHabi(int id);
    }
}
