
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

        public async Task<List<Publicaciones>> GetPublicacionesInfoComp()
        {
            //return await _context.Publicaciones
            //.Include(p => p.Usuarios)
            //.Include(p => p.PublModa).ThenInclude(pm => pm.Modalidades)
            //.Include(p => p.PublHabi).ThenInclude(ph => ph.Habilidades)
            //.Select(p => new Publicaciones
            //{
            //    IdPubl = p.IdPubl,
            //    TituloPubl = p.TituloPubl,
            //    NombreUsua = p.Usuarios.NombreUsua,
            //    NombreModa = p.PublModa.Modalidades.NombreModa,
            //    NombreHabi = p.PublHabi.Habilidades.NombreHabi
            //})
            //.ToListAsync();
            return await GetPublicaciones();
        }

        public async Task<Publicaciones> GetPublicacionesById(int id)
        {
            return await _context.Publicaciones.FindAsync(id);
        }

        public async Task<Publicaciones> GetPublicacionesInfoCompById(int id)
        {
            //return await _context.Publicaciones
            //.Where(p => p.IdPubl == id)
            //.Include(p => p.Usuarios)
            //.Include(p => p.PublModa).ThenInclude(pm => pm.Modalidades)
            //.Include(p => p.PublHabi).ThenInclude(ph => ph.Habilidades)
            //.Select(p => new Publicaciones
            //{
            //    IdPubl = p.IdPubl,
            //    TituloPubl = p.TituloPubl,
            //    NombreUsua = p.Usuarios.NombreUsua,
            //    NombreModa = p.PublModa.Select(pm => pm.Modalidades.NombreModa).FirstOrDefault(),
            //    NombreHabi = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).FirstOrDefault()
            //})
            //.FirstOrDefaultAsync();
            return await GetPublicacionesById(id);
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
