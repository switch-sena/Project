
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

        public async Task<Publicaciones> GetPublicacionById(int id)
        {
            return await _context.Publicaciones.FindAsync(id);
        }

        public async Task<bool> PostPublicacion(Publicaciones publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            return await _context.SaveAsync();
        }

        public async Task<bool> PutPublicacion(int id, Publicaciones publicacion)
        {
            var existingPublicacion = await _context.Publicaciones.FindAsync(id);
            if (existingPublicacion == null) return false;

            existingPublicacion.TituloPubl = publicacion.TituloPubl;
            existingPublicacion.DescripcionPubl = publicacion.DescripcionPubl;
            existingPublicacion.CopiaIdUsua = publicacion.CopiaIdUsua;

            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublicacion(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null) return false;

            _context.Publicaciones.Remove(publicacion);
            return await _context.SaveAsync();
        }
    }
}
