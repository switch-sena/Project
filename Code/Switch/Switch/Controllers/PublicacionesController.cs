
using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly IPublicacionesRepository _repository;

        public PublicacionesController(IPublicacionesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicaciones()
        {
            var response = await _repository.GetPublicaciones();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicacionById(int id)
        {
            var response = await _repository.GetPublicacionById(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostPublicacion([FromBody] Publicaciones publicacion)
        {
            var result = await _repository.PostPublicacion(publicacion);
            if (result)
                return Ok("Publicación creada correctamente.");
            return BadRequest("Error al crear la publicación.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacion(int id, [FromBody] Publicaciones publicacion)
        {
            var result = await _repository.PutPublicacion(id, publicacion);
            if (result)
                return Ok("Publicación actualizada correctamente.");
            return BadRequest("Error al actualizar la publicación.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var result = await _repository.DeletePublicacion(id);
            if (result)
                return Ok("Publicación eliminada correctamente.");
            return BadRequest("Error al eliminar la publicación.");
        }
    }
}
