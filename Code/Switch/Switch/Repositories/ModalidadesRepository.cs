using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SwitchBack.Repositories
{
    public class ModalidadesRepository : IModalidadesRepository
    {
        private readonly SwitchContext _context;
        public ModalidadesRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<Modalidades>> GetModalidades()
        {
            var data = await _context.Modalidades.ToListAsync();
            return data;
        }
        public async Task<Modalidades> GetModalidadesById(int id)
        {
            var data = await _context.Modalidades.FindAsync(id);
            return data;
        }

        public async Task<bool> PostModalidades(Modalidades modalidades)
        {
            await _context.Modalidades.AddAsync(modalidades);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateModalidades(Modalidades modalidades)
        {
            _context.Modalidades.Update(modalidades);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeleteModalidades(int id)
        {
            var Modalidad = await _context.Modalidades.FindAsync(id);
            if (Modalidad == null)
            {
                return false;
            }
            _context.Modalidades.Remove(Modalidad);
            await _context.SaveAsync();
            return true;
        }
    }
}
