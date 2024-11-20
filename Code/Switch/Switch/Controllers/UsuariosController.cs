using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;

        public UsuariosController(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _repository.GetUsuarios();
            return Ok(response);
        }

        [HttpGet("GetUsuariosById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsuariosById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var usuario = await _repository.GetUsuariosById(id);
            if (usuario == null)
            {
                return NotFound($"No se encontró el usuario con ID {id}.");
            }
            return Ok(usuario);
        }

        [HttpPost("PostUsuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUsuarios([FromBody] Usuarios usuario)
        {
            if (usuario == null) return BadRequest("El objeto usuario no puede ser nulo.");

            try
            {
                var created = await _repository.PostUsuarios(usuario);
                if (created)
                    return CreatedAtAction(nameof(GetUsuariosById), new { id = usuario.IdUsua }, usuario);
                return BadRequest("Error al crear el usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateUsuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUsuarios(int id, [FromBody] Usuarios usuario)
        {
            if (usuario == null) return BadRequest("El objeto usuario no puede ser nulo.");
            if (id != usuario.IdUsua) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            var updated = await _repository.UpdateUsuarios(usuario);
            if (updated)
                return NoContent();

            return NotFound($"No se encontró el usuario con ID {id}.");
        }

        [HttpDelete("DeleteUsuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var deleted = await _repository.DeleteUsuarios(id);
            if (deleted) return NoContent();
            return NotFound($"No se encontró el usuario con ID {id}.");
        }
    }
}