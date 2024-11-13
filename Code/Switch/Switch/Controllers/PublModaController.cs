using SwitchBack.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SwitchBack.Models;
using Microsoft.AspNetCore.Authorization;

namespace SwitchBack.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublModaController : ControllerBase
    {
        private readonly IPublModaRepository _repository;

        public PublModaController(IPublModaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetPublModa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublModa()
        {
            var response = await _repository.GetPublModa();
            return Ok(response);
        }

        [HttpGet("GetPublModaById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublModaById(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            var publmoda = await _repository.GetPublModaById(id);
            if (publmoda == null) return NotFound($"No se encontró la publicación de modalidad con ID {id}.");
            return Ok(publmoda);
        }

        [HttpPost("PostPublModa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublModa([FromBody] PublModa publmoda)
        {
            if (publmoda == null) return BadRequest("El objeto PublModa no puede ser nulo.");

            try
            {
                var response = await _repository.PostPublModa(publmoda);
                if (response)
                {
                    return CreatedAtAction(nameof(GetPublModaById), new { id = publmoda.Id }, publmoda);
                }
                return BadRequest("Error al crear la publicación de modalidad.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdatePublModa/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePublModa(int id, [FromBody] PublModa publmoda)
        {
            if (publmoda == null) return BadRequest("El objeto PublModa no puede ser nulo.");
            if (id != publmoda.Id) return BadRequest("El ID en la URL no coincide con el ID del objeto.");

            try
            {
                var response = await _repository.UpdatePublModa(id, publmoda);
                if (response) return NoContent();
                return NotFound($"No se encontró la publicación de modalidad con ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeletePublModa/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePublModa(int id)
        {
            if (id <= 0) return BadRequest("El ID debe ser mayor que cero.");

            try
            {
                var response = await _repository.DeletePublModa(id);
                if (response) return NoContent();
                return NotFound($"No se encontró la publicación de modalidad con ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}