
using Microsoft.EntityFrameworkCore;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Repositories
{
    public class PublicacionesRepository : IPublicacionesRepository
    {
        private readonly SwitchContext _context;

        public PublicacionesRepository(SwitchContext context)
        {
            _context = context;
        }

        public async Task<List<Publicaciones>> GetPublicaciones()
        {
            return await _context.Publicaciones.ToListAsync();
        }

        public async Task<List<PublicacionesDTO>> GetPublicacionesInfoComp()
        {
            return await _context.Publicaciones
                .Include(p => p.Usuarios) // Incluye la relación con Usuarios
                .Include(p => p.PublModa).ThenInclude(pm => pm.Modalidades) // Incluye la relación con Modalidades
                .Include(p => p.PublHabi).ThenInclude(ph => ph.Habilidades) // Incluye la relación con Habilidades
                .Select(p => new PublicacionesDTO
                {
                    IdPubl = p.IdPubl,
                    TituloPubl = p.TituloPubl,
                    DescripcionPubl = p.DescripcionPubl,
                    NombreUsuario = p.Usuarios.NombreUsua + " " + p.Usuarios.ApellidoUsua, // Nombre completo del usuario
                    Habilidades = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).ToList(), // Lista de habilidades
                    Modalidades = p.PublModa.Select(pm => pm.Modalidades.NombreModa).ToList() // Lista de modalidades
                })
                .ToListAsync();
        }

        public async Task<Publicaciones> GetPublicacionesById(int id)
        {
            return await _context.Publicaciones.FindAsync(id);
        }

        public async Task<List<PublicacionesDTO>> GetPublicacionesInfoCompByUsuario(int idUsua)
        {
            return await _context.Publicaciones
                .Where(p => p.CopiaIdUsua == idUsua) // Filtro por IdUsua
                .Include(p => p.Usuarios) // Incluye la relación con Usuarios
                .Include(p => p.PublModa).ThenInclude(pm => pm.Modalidades) // Incluye la relación con Modalidades
                .Include(p => p.PublHabi).ThenInclude(ph => ph.Habilidades) // Incluye la relación con Habilidades
                .Select(p => new PublicacionesDTO
                {
                    IdPubl = p.IdPubl,
                    TituloPubl = p.TituloPubl,
                    DescripcionPubl = p.DescripcionPubl,
                    NombreUsuario = p.Usuarios.NombreUsua + " " + p.Usuarios.ApellidoUsua, // Nombre completo del usuario
                    Habilidades = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).ToList(), // Lista de habilidades
                    Modalidades = p.PublModa.Select(pm => pm.Modalidades.NombreModa).ToList() // Lista de modalidades
                })
                .ToListAsync();
        }


        public async Task<bool> PostPublicaciones(Publicaciones publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            return await _context.SaveAsync();
        }

        public async Task<bool> UpdatePublicaciones(int id, Publicaciones publicacion)
        {
            var existingPublicacion = await _context.Publicaciones.FindAsync(id);
            if (existingPublicacion == null) return false;

            existingPublicacion.TituloPubl = publicacion.TituloPubl;
            existingPublicacion.DescripcionPubl = publicacion.DescripcionPubl;
            existingPublicacion.CopiaIdUsua = publicacion.CopiaIdUsua;

            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublicaciones(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null) return false;

            _context.Publicaciones.Remove(publicacion);
            return await _context.SaveAsync();
        }
    }
}
