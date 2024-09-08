using Microsoft.EntityFrameworkCore;
using Switch.Models;
using Switch.Repositories.Interfaces;

namespace Switch.Repositories
{
    public class PublHabiRepository : IPublHabiRepository
    {
        private readonly SwitchContext _context;

        public PublHabiRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<PublHabi>> GetPublHabi()
        {
            return await _context.PublHabi.ToListAsync();
        }

        public async Task<bool> CreatePublHabi(PublHabi publHabi)
        {
            await _context.PublHabi.AddAsync(publHabi);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdatePublHabi(int id, PublHabi publHabi)
        {
            var existingPublHabi = await _context.PublHabi.FindAsync(id);
            if (existingPublHabi == null) return false;

            existingPublHabi.CopiaIdPubl = publHabi.CopiaIdPubl;
            existingPublHabi.CopiaIdHabi = publHabi.CopiaIdHabi;

            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublHabi(int id)
        {
            var publHabi = await _context.PublHabi.FindAsync(id);
            if (publHabi == null) return false;

            _context.PublHabi.Remove(publHabi);
            return await _context.SaveAsync();
        }
    }
}
