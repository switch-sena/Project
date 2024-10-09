
using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
{
    public class PublModaRepository : IPublModaRepository
    {
        private readonly SwitchContext _context;

        public PublModaRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<PublModa>> GetPublModa()
        {
            return await _context.PublModa.ToListAsync();
        }

        public async Task<bool> PostPublModa(PublModa publModa)
        {
            await _context.PublModa.AddAsync(publModa);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdatePublModa(int id, PublModa publModa)
        {
            var existingPublModa = await _context.PublModa.FindAsync(id);
            if (existingPublModa == null) return false;

            existingPublModa.CopiaIdPubl = publModa.CopiaIdPubl;
            existingPublModa.CopiaIdModa = publModa.CopiaIdModa;

            _context.PublModa.Update(existingPublModa);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublModa(int id)
        {
            var publModa = await _context.PublModa.FindAsync(id);
            if (publModa == null) return false;

            _context.PublModa.Remove(publModa);
            return await _context.SaveAsync();
        }
    }
}
