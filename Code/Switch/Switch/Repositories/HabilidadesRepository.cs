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
            return await _context.Habilidades.ToListAsync();
        }

        public async Task<Habilidades> GetHabilidadById(int id)
        {
            return await _context.Habilidades.FindAsync(id);
        }

        public async Task<bool> AddHabilidad(Habilidades habilidad)
        {
            await _context.Habilidades.AddAsync(habilidad);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdateHabilidad(Habilidades habilidad)
        {
            _context.Habilidades.Update(habilidad);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteHabilidad(int id)
        {
            var habilidad = await _context.Habilidades.FindAsync(id);
            if (habilidad == null) return false;
            _context.Habilidades.Remove(habilidad);
            return await _context.SaveAsync();
        }
    }
}
