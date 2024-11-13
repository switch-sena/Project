using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModalidadesController : ControllerBase
    {
        private readonly IModalidadesRepository _repository;

        public ModalidadesController(IModalidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetModalidades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetModalidades()
        {
            var response = await _repository.GetModalidades();
            return Ok(response);
        }

        [HttpGet("GetModalidadesById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetModalidadesById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var modalidades = await _repository.GetModalidadesById(id);
            if (modalidades == null) return NotFound($"No se encontró la modalidad con ID {id}.");
            return Ok(modalidades);
        }

        [HttpPost("PostModalidades")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostModalidades([FromBody] Modalidades modalidades)
        {
            if (modalidades == null) return BadRequest("El objeto modalidad no puede ser nulo.");

            try
            {
                var response = await _repository.PostModalidades(modalidades);
                if (response)
                {
                    return CreatedAtAction(nameof(GetModalidadesById), new { id = modalidades.IdModa }, modalidades);
                }
                return BadRequest("Error al insertar la modalidad.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateModalidades/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateModalidades(int id, [FromBody] Modalidades modalidades)
        {
            if (modalidades == null) return BadRequest("El objeto modalidad no puede ser nulo.");
            if (id != modalidades.IdModa) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            var result = await _repository.UpdateModalidades(modalidades);
            if (result) return NoContent();
            return NotFound($"No se encontró la modalidad con ID {id}.");
        }

        [HttpDelete("DeleteModalidades/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteModalidades(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var result = await _repository.DeleteModalidades(id);
            if (result) return NoContent();
            return NotFound($"No se encontró la modalidad con ID {id}.");
        }
    }
}
