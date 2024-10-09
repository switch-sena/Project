using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
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
            //Preguntar al profe si de esta manera se puede acortar el codigo
            return await _context.PublHabi.ToListAsync();
        }

        public async Task<bool> PostPublHabi(PublHabi publHabi)
        {
            //Preguntar al profe si de esta manera se puede acortar el codigo
            await _context.PublHabi.AddAsync(publHabi);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdatePublHabi(int id, PublHabi publHabi)
        {
            //Preguntar funcionalidad a duveimar
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
