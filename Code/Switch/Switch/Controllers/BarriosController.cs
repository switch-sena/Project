using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BarriosController : ControllerBase
    {
        private readonly IBarriosRepository _repository;

        public BarriosController(IBarriosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetBarrios")]
        [AllowAnonymous] // Permitir acceso sin autenticación
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBarrios()
        {
            var response = await _repository.GetBarrios();
            return Ok(response);
        }

        [HttpGet("GetBarriosById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBarriosById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var barrio = await _repository.GetBarriosById(id);
            if (barrio == null) return NotFound($"No se encontró el barrio con ID {id}.");
            return Ok(barrio);
        }

        [HttpPost("PostBarrios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostBarrios([FromBody] Barrios barrios)
        {
            if (barrios == null) return BadRequest("El objeto barrio no puede ser nulo.");

            try
            {
                var response = await _repository.PostBarrios(barrios);
                if (response)
                {
                    return CreatedAtAction(nameof(GetBarriosById), new { id = barrios.IdBarr }, barrios);
                }
                return BadRequest("Error al insertar el barrio.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateBarrios/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBarrios(int id, [FromBody] Barrios barrios)
        {
            if (barrios == null) return BadRequest("El objeto barrio no puede ser nulo.");
            if (id != barrios.IdBarr) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            var result = await _repository.UpdateBarrios(barrios);
            if (result) return NoContent();
            return NotFound($"No se encontró el barrio con ID {id}.");
        }

        [HttpDelete("DeleteBarrios/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBarrios(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var result = await _repository.DeleteBarrios(id);
            if (result) return NoContent();
            return NotFound($"No se encontró el barrio con ID {id}.");
        }
    }
}
