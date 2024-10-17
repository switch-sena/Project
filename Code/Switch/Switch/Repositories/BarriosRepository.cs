using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
{
    public class BarriosRepository : IBarriosRepository
    {
        private readonly SwitchContext _context;

        public BarriosRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<Barrios>> GetBarrios()
        {
            var data = await _context.Barrios.ToListAsync();
            return data;
        }
        public async Task<Barrios> GetBarriosById(int id)
        {
            var data = await _context.Barrios.FindAsync(id);
            return data;
        }

        public async Task<bool> PostBarrios(Barrios barrios)
        {
            await _context.Barrios.AddAsync(barrios);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateBarrios(Barrios barrios)
        {
            //preguntar si en esta parte hace falta "await"
            _context.Barrios.Update(barrios);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteBarrios(int id)
        {
            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return false;
            }
            //preguntar si en esta parte hace falta "await"
            _context.Barrios.Remove(barrio);
            await _context.SaveAsync();
            return true;
        }

    }
}
