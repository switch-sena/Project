using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SwitchBack.Repositories;
using System.Security.Claims;

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

        [HttpGet("GetPublicacionesInfoCompByPubl/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicacionesInfoCompByPubl(int id)
        {
            var publicacion = await _repository.GetPublicacionesInfoCompByPubl(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }

        [HttpGet("GetPublicacionesInfoCompByUsuario/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublicacionesInfoCompByUsuario(int id)
        {
            var publicacion = await _repository.GetPublicacionesInfoCompByUsuario(id);
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

        [HttpPost("PostPublicacionesDTO")]
        public async Task<IActionResult> PostPublicacionesDTO([FromBody] PublicacionesDTO publicacionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Obtener el ID del usuario desde el token
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("Token inválido o no contiene información del usuario.");
                }

                if (!int.TryParse(userIdClaim.Value, out int id))
                {
                    return BadRequest("El ID del usuario en el token no es válido.");
                }

                var result = await _repository.PostPublicacionesDTO(publicacionDTO, id);
                if (result)
                    return Ok(new { message = "Publicación creada exitosamente." });

                return StatusCode(500, new { message = "Error al crear la publicación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno.", error = ex.Message });
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

        [HttpPut("UpdatePublicacionesDTO/{id}")]
        public async Task<IActionResult> UpdatePublicacionesDTO(int id, [FromBody] PublicacionesDTO publicaciones)
        {
            if (id != publicaciones.IdPubl)
                return BadRequest("El ID de la publicación no coincide con el parámetro.");

            var result = await _repository.UpdatePublicacionesDTO(publicaciones);
            if (!result)
                return NotFound("Publicación no encontrada o no se pudo actualizar.");

            return NoContent();
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

        [HttpDelete("DeletePublicacionesDTO/{id}")]
        public async Task<IActionResult> DeletePublicacionesDTO(int id)
        {
            var resultado = await _repository.DeletePublicacionesDTO(id);

            if (!resultado)
            {
                return NotFound(new { mensaje = "La publicación no existe o ya fue eliminada." });
            }

            return Ok(new { mensaje = "La publicación y sus relaciones fueron eliminadas correctamente." });
        }
    }
}
