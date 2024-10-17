
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

        public async Task<PublModa> GetPublModaById(int id)
        {
            var data = await _context.PublModa.FindAsync(id);
            return data;
        }

        public async Task<bool> PostPublModa(PublModa publmoda)
        {
            await _context.PublModa.AddAsync(publmoda);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdatePublModa(int id, PublModa publmoda)
        {
            var existingPublModa = await _context.PublModa.FindAsync(id);
            if (existingPublModa == null) return false;

            existingPublModa.CopiaIdPubl = publmoda.CopiaIdPubl;
            existingPublModa.CopiaIdModa = publmoda.CopiaIdModa;

            _context.PublModa.Update(existingPublModa);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublModa(int id)
        {
            var publmoda = await _context.PublModa.FindAsync(id);
            if (publmoda == null) return false;

            _context.PublModa.Remove(publmoda);
            return await _context.SaveAsync();
        }
    }
}
