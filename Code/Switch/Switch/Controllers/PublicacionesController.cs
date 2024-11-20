
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

        [HttpGet("GetPublicaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicaciones()
        {
            var response = await _repository.GetPublicaciones();
            return Ok(response);
        }

        [HttpGet("GetPublicacionesInfoComp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicacionesInfoComp()
        {
            var publicaciones = await _repository.GetPublicacionesInfoComp();
            return Ok(publicaciones);
        }

        [HttpGet("GetPublicacionesById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicacionesById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var response = await _repository.GetPublicacionesById(id);
            if (response == null) return NotFound($"No se encontró la publicación con ID {id}.");
            return Ok(response);
        }

        [HttpGet("GetPublicacionesInfoCompById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicacionesInfoCompById(int id)
        {
            var publicacion = await _repository.GetPublicacionesInfoCompById(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }

        [HttpPost("PostPublicaciones")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublicaciones([FromBody] Publicaciones publicacion)
        {
            if (publicacion == null) return BadRequest("El objeto publicación no puede ser nulo.");

            try
            {
                var result = await _repository.PostPublicaciones(publicacion);
                if (result)
                {
                    return CreatedAtAction(nameof(GetPublicacionesById), new { id = publicacion.IdPubl }, publicacion);
                }
                return BadRequest("Error al crear la publicación.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdatePublicaciones/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePublicaciones(int id, [FromBody] Publicaciones publicacion)
        {
            if (publicacion == null) return BadRequest("El objeto publicación no puede ser nulo.");
            if (id != publicacion.IdPubl) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            var result = await _repository.UpdatePublicaciones(id, publicacion);
            if (result) return NoContent();
            return NotFound($"No se encontró la publicación con ID {id}.");
        }

        [HttpDelete("DeletePublicaciones/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePublicaciones(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var result = await _repository.DeletePublicaciones(id);
            if (result) return NoContent();
            return NotFound($"No se encontró la publicación con ID {id}.");
        }
    }
}
