using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly SwitchContext _context;

        public UsuariosRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<Usuarios>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuarios> GetUsuariosById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuarios> GetUsuariosByEmail(string email)
        {
            return await _context.Usuarios.Where(x => x.CorreoUsua == email).FirstOrDefaultAsync();
        }

        public async Task<bool> PostUsuarios(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUsuarios(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
