using Microsoft.EntityFrameworkCore;
using Switch.Models;
using Switch.Repositories.Interfaces;

namespace Switch.Repositories
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

        public async Task<Habilidades> GetHabilidadById(int id)
        {
            var data = await _context.Habilidades.FindAsync(id);
            return data;
        }

        public async Task<bool> PostHabilidad(Habilidades habilidad)
        {
            await _context.Habilidades.AddAsync(habilidad);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateHabilidad(Habilidades habilidad)
        {
            //preguntar si en esta parte hace falta "await"
            _context.Habilidades.Update(habilidad);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteHabilidad(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);
            if (habilidad == null)
            {
                return false;
            }
            //preguntar si en esta parte hace falta "await"
            _context.Habilidades.Remove(habilidad);
            await _context.SaveAsync();
            return true;
        }
    }
}
