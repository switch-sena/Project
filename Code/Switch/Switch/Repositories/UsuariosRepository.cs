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

        public async Task<List<UsuariosDTO>> GetUsuariosDTO()
        {
            return await _context.Usuarios.Include(p => p.Barrio).
                Select(p => new UsuariosDTO
                {
                    IdUsua = p.IdUsua,
                    NombreUsua = p.NombreUsua,
                    ApellidoUsua = p.ApellidoUsua,
                    GeneroUsua = p.GeneroUsua,
                    FechaNacimientoUsua = p.FechaNacimientoUsua,
                    CelularUsua = p.CelularUsua,
                    CorreoUsua = p.CorreoUsua,
                    ClaveUsua = p.ClaveUsua,
                    CorreoElectronicoUsua = p.CorreoElectronicoUsua,
                    LinksRsUsua = p.LinksRsUsua,
                    Barrios = p.Barrio.NombreBarr,
                    IdBarr = p.Barrio.IdBarr
                })
                .ToListAsync();
        }

        public async Task<UsuariosDTO> GetUsuariosDTOById(int id)
        {
            return await _context.Usuarios
                .Where(p => p.IdUsua == id)
                .Include(p => p.Barrio)
                .Select(p => new UsuariosDTO
                {
                    IdUsua = p.IdUsua,
                    NombreUsua = p.NombreUsua,
                    ApellidoUsua = p.ApellidoUsua,
                    GeneroUsua = p.GeneroUsua,
                    FechaNacimientoUsua = p.FechaNacimientoUsua,
                    CelularUsua = p.CelularUsua,
                    CorreoUsua = p.CorreoUsua,
                    ClaveUsua = p.ClaveUsua,
                    CorreoElectronicoUsua = p.CorreoElectronicoUsua,
                    LinksRsUsua = p.LinksRsUsua,
                    Barrios = p.Barrio.NombreBarr,
                    IdBarr = p.Barrio.IdBarr
                }).FirstAsync();
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

        public async Task<bool> PostUsuariosDTO(UsuariosDTO usuarioDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var usuarios = new Usuarios
                {
                    IdUsua = usuarioDTO.IdUsua,
                    NombreUsua = usuarioDTO.NombreUsua,
                    ApellidoUsua = usuarioDTO.ApellidoUsua,
                    GeneroUsua = usuarioDTO.GeneroUsua,
                    FechaNacimientoUsua = usuarioDTO.FechaNacimientoUsua,
                    CelularUsua = usuarioDTO.CelularUsua,
                    CorreoUsua = usuarioDTO.CorreoUsua,
                    ClaveUsua = usuarioDTO.ClaveUsua,
                    CorreoElectronicoUsua = usuarioDTO.CorreoElectronicoUsua,
                    LinksRsUsua = usuarioDTO.LinksRsUsua,
                    CopiaIdBarr = usuarioDTO.IdBarr
                };

                _context.Usuarios.Add(usuarios);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }            
        }

        public async Task<bool> UpdateUsuarios(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUsuariosDTO(UsuariosDTO usuarios)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(p => p.IdUsua == usuarios.IdUsua);

            if (usuario == null)
                return false;

            // actualiuzar los campos principales
            usuario.NombreUsua = usuarios.NombreUsua ?? usuario.NombreUsua;
            usuario.ApellidoUsua = usuarios.ApellidoUsua ?? usuario.ApellidoUsua; 
            usuario.GeneroUsua  = usuarios.GeneroUsua ?? usuario.GeneroUsua;
            usuario.FechaNacimientoUsua = usuario.FechaNacimientoUsua;
            usuario.CelularUsua = usuarios.CelularUsua ?? usuario.CelularUsua;
            usuario.CorreoUsua = usuarios.CorreoUsua ?? usuario.CorreoUsua;
            usuario.ClaveUsua = usuarios.ClaveUsua ?? usuario.ClaveUsua;
            usuario.CorreoElectronicoUsua = usuario.CorreoElectronicoUsua ?? usuario.CorreoElectronicoUsua;
            usuario.LinksRsUsua = usuario.LinksRsUsua ?? usuario.LinksRsUsua;
            usuario.CopiaIdBarr = usuarios.IdBarr;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuariosDTO(int id)
        {
            // Obtener la publicación y validar su existencia
            var usuario = await _context.Usuarios
                .Include(p => p.Barrio)
                .FirstOrDefaultAsync(p => p.IdUsua == id);
            if (usuario == null)
            {
                return false; // la publicacion no existe
            }

            _context.Usuarios .Remove(usuario);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
