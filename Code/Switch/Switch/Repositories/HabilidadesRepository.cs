using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
{
    public class HabilidadesRepository : IHabilidadesRepository
    {
        private readonly SwitchContext _context;

        public HabilidadesRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<Habilidades>> GetHabilidades()
        {
            var data = await _context.Habilidades.ToListAsync();
            return data;
        }

        public async Task<Habilidades> GetHabilidadesById(int id)
        {
            var data = await _context.Habilidades.FindAsync(id);
            return data;
        }

        public async Task<bool> PostHabilidades(Habilidades habilidad)
        {
            await _context.Habilidades.AddAsync(habilidad);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateHabilidades(Habilidades habilidad)
        {
            _context.Habilidades.Update(habilidad);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteHabilidades(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);
            if (habilidad == null)
            {
                return false;
            }
            _context.Habilidades.Remove(habilidad);
            await _context.SaveAsync();
            return true;
        }
    }
}
