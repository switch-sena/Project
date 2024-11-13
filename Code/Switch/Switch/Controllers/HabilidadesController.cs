using Microsoft.AspNetCore.Mvc;
using SwitchBack.Repositories.Interfaces;
using SwitchBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadesController : ControllerBase
    {
        private readonly IHabilidadesRepository _repository;

        public HabilidadesController(IHabilidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetHabilidades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHabilidades()
        {
            var habilidades = await _repository.GetHabilidades();
            return Ok(habilidades);
        }

        [HttpGet("GetHabilidadesById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHabilidadesById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var habilidad = await _repository.GetHabilidadesById(id);
            if (habilidad == null) return NotFound($"No se encontró la habilidad con ID {id}.");
            return Ok(habilidad);
        }

        [HttpPost("PostHabilidades")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostHabilidades([FromBody] Habilidades habilidad)
        {
            if (habilidad == null) return BadRequest("El objeto habilidad no puede ser nulo.");

            var result = await _repository.PostHabilidades(habilidad);
            if (result)
            {
                return CreatedAtAction(nameof(GetHabilidadesById), new { id = habilidad.IdHabi }, habilidad);
            }
            return BadRequest("Error al crear la habilidad.");
        }

        [HttpPut("UpdateHabilidades/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateHabilidades(int id, [FromBody] Habilidades habilidad)
        {
            if (habilidad == null) return BadRequest("El objeto habilidad no puede ser nulo.");
            if (id != habilidad.IdHabi) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            var result = await _repository.UpdateHabilidades(habilidad);
            if (result) return NoContent();
            return NotFound($"No se encontró la habilidad con ID {id}.");
        }

        [HttpDelete("DeleteHabilidades/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteHabilidades(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var result = await _repository.DeleteHabilidades(id);
            if (result) return NoContent();
            return NotFound($"No se encontró la habilidad con ID {id}.");
        }
    }
}
