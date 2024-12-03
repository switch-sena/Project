
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
                    CopiaIdUsua = p.Usuarios.IdUsua, // el Id del usuario
                    Habilidades = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).ToList(), // Lista de habilidades
                    Modalidades = p.PublModa.Select(pm => pm.Modalidades.NombreModa).ToList(), // Lista de modalidades
                    IdHabi = p.PublHabi.Select(ph => ph.Habilidades.IdHabi).ToList(), // Lista de los Id de las habilidades
                    IdModa = p.PublModa.Select(pm => pm.Modalidades.IdModa).ToList()  // Lista de los Id de las modalidades
                })
                .ToListAsync();
        }

        public async Task<Publicaciones> GetPublicacionesById(int id)
        {
            return await _context.Publicaciones.FindAsync(id);
        }

        public async Task<PublicacionesDTO> GetPublicacionesInfoCompByPubl(int id)
        {
            return await _context.Publicaciones
                .Where(p => p.IdPubl == id ) // Filtro por IdPubl
                .Include(p => p.Usuarios) // Incluye la relación con Usuarios
                .Include(p => p.PublModa).ThenInclude(pm => pm.Modalidades) // Incluye la relación con Modalidades
                .Include(p => p.PublHabi).ThenInclude(ph => ph.Habilidades) // Incluye la relación con Habilidades
                .Select(p => new PublicacionesDTO
                {
                    IdPubl = p.IdPubl,
                    TituloPubl = p.TituloPubl,
                    DescripcionPubl = p.DescripcionPubl,
                    NombreUsuario = p.Usuarios.NombreUsua + " " + p.Usuarios.ApellidoUsua, // Nombre completo del usuario
                    CopiaIdUsua = p.Usuarios.IdUsua, // el Id del usuario
                    Habilidades = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).ToList(), // Lista de habilidades
                    Modalidades = p.PublModa.Select(pm => pm.Modalidades.NombreModa).ToList(), // Lista de modalidades
                    IdHabi = p.PublHabi.Select(ph => ph.Habilidades.IdHabi).ToList(), // Lista de los Id de las habilidades
                    IdModa = p.PublModa.Select(pm => pm.Modalidades.IdModa).ToList()  // Lista de los Id de las modalidades
                })
                .FirstOrDefaultAsync();
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
                    CopiaIdUsua = p.Usuarios.IdUsua, // el Id del usuario
                    Habilidades = p.PublHabi.Select(ph => ph.Habilidades.NombreHabi).ToList(), // Lista de habilidades
                    Modalidades = p.PublModa.Select(pm => pm.Modalidades.NombreModa).ToList(), // Lista de modalidades
                    IdHabi = p.PublHabi.Select(ph => ph.Habilidades.IdHabi).ToList(), // Lista de los Id de las habilidades
                    IdModa = p.PublModa.Select(pm => pm.Modalidades.IdModa).ToList()  // Lista de los Id de las modalidades
                })
                .ToListAsync();
        }


        public async Task<bool> PostPublicaciones(Publicaciones publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            return await _context.SaveAsync();
        }

        public async Task<bool> PostPublicacionesDTO(PublicacionesDTO publicacionDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear la publicación
                var publicacion = new Publicaciones
                {
                    TituloPubl = publicacionDTO.TituloPubl,
                    DescripcionPubl = publicacionDTO.DescripcionPubl,
                    CopiaIdUsua = publicacionDTO.CopiaIdUsua
                };

                _context.Publicaciones.Add(publicacion);
                await _context.SaveChangesAsync();

                // Agregar habilidades relacionadas
                foreach (var IdHabi in publicacionDTO.IdHabi)
                {
                    var publHabi = new PublHabi
                    {
                        CopiaIdPubl = publicacion.IdPubl,
                        CopiaIdHabi = IdHabi
                    };
                    _context.PublHabi.Add(publHabi);
                }

                // Agregar modalidades relacionadas
                foreach (var IdModa in publicacionDTO.IdModa)
                {
                    var publModa = new PublModa
                    {
                        CopiaIdPubl = publicacion.IdPubl,
                        CopiaIdModa = IdModa
                    };
                    _context.PublModa.Add(publModa);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task<bool> UpdatePublicacionesDTO(PublicacionesDTO publicaciones)
        {
            var publicacion = await _context.Publicaciones
                .Include(p => p.PublHabi)
                .Include(p => p.PublModa)
                .FirstOrDefaultAsync(p => p.IdPubl == publicaciones.IdPubl);

            if (publicacion == null)
                return false;

            // Actualizar los campos principales
            publicacion.TituloPubl = publicaciones.TituloPubl ?? publicacion.TituloPubl;
            publicacion.DescripcionPubl = publicaciones.DescripcionPubl ?? publicacion.DescripcionPubl;
            publicacion.CopiaIdUsua = publicacion.CopiaIdUsua;

            // Actualizar las habilidades
            if (publicaciones.IdHabi != null)
            {
                _context.PublHabi.RemoveRange(publicacion.PublHabi);
                foreach (var IdHabi in publicaciones.IdHabi)
                {
                    publicacion.PublHabi.Add(new PublHabi { CopiaIdHabi = IdHabi, CopiaIdPubl = publicaciones.IdPubl });
                }
            }

            // Actualizar las modalidades
            if (publicaciones.IdModa != null)
            {
                _context.PublModa.RemoveRange(publicacion.PublModa);
                foreach (var IdModa in publicaciones.IdModa)
                {
                    publicacion.PublModa.Add(new PublModa { CopiaIdModa = IdModa, CopiaIdPubl = publicaciones.IdPubl });
                }
            }

            _context.Publicaciones.Update(publicacion);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeletePublicaciones(int id)
        {
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null) return false;

            _context.Publicaciones.Remove(publicacion);
            return await _context.SaveAsync();
        }

        public async Task<bool> DeletePublicacionesDTO(int id)
        {
            // Obtener la publicación y validar su existencia
            var publicacion = await _context.Publicaciones
                .Include(p => p.PublHabi)
                .Include(p => p.PublModa)
                .FirstOrDefaultAsync(p => p.IdPubl == id);

            if (publicacion == null)
            {
                return false; // La publicación no existe
            }

            // Eliminar las relaciones de habilidades (PublHabi)
            if (publicacion.PublHabi != null && publicacion.PublHabi.Any())
            {
                _context.PublHabi.RemoveRange(publicacion.PublHabi);
            }

            // Eliminar las relaciones de modalidades (PublModa)
            if (publicacion.PublModa != null && publicacion.PublModa.Any())
            {
                _context.PublModa.RemoveRange(publicacion.PublModa);
            }

            // Eliminar la publicación principal
            _context.Publicaciones.Remove(publicacion);

            // Guardar los cambios
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
