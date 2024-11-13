using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublHabiController : ControllerBase
    {
        private readonly IPublHabiRepository _repository;

        public PublHabiController(IPublHabiRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los PublHabi
        [HttpGet("GetPublHabi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublHabi()
        {
            var response = await _repository.GetPublHabi();
            return Ok(response);
        }

        [HttpGet("GetPublHabiById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublHabiById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var publhabi = await _repository.GetPublHabiById(id);
            if (publhabi == null) return NotFound($"No se encontró el PublHabi con ID {id}.");
            return Ok(publhabi);
        }

        // Crear nuevo PublHabi
        [HttpPost("PostPublHabi")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublHabi([FromBody] PublHabi publhabi)
        {
            if (publhabi == null) return BadRequest("El objeto PublHabi no puede ser nulo.");

            try
            {
                var response = await _repository.PostPublHabi(publhabi);
                if (response)
                {
                    return CreatedAtAction(nameof(GetPublHabiById), new { id = publhabi.Id }, publhabi);
                }
                return BadRequest("Error al crear PublHabi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Actualizar PublHabi
        [HttpPut("UpdatePublHabi/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePublHabi(int id, [FromBody] PublHabi publhabi)
        {
            if (publhabi == null) return BadRequest("El objeto PublHabi no puede ser nulo.");
            if (id != publhabi.Id) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            try
            {
                var response = await _repository.UpdatePublHabi(id, publhabi);
                if (response) return NoContent();
                return NotFound($"No se encontró el PublHabi con ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Eliminar PublHabi
        [HttpDelete("DeletePublHabi/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePublHabi(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            try
            {
                var response = await _repository.DeletePublHabi(id);
                if (response) return NoContent();
                return NotFound($"No se encontró el PublHabi con ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}