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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPublHabi()
        {
            var response = await _repository.GetPublHabi();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublHabiById(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var publhabi = await _repository.GetPublHabiById(id);
            if (publhabi == null) return NotFound();
            return Ok(publhabi);
        }

        // Crear nuevo PublHabi
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublHabi([FromBody] PublHabi publhabi)
        {
            try
            {
                var response = await _repository.PostPublHabi(publhabi);
                if (response)
                    return CreatedAtAction(nameof(GetPublHabi), new { id = publhabi.Id }, publhabi);
                return BadRequest("Error al crear PublHabi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualizar PublHabi
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePublHabi(int id, [FromBody] PublHabi publhabi)
        {
            try
            {
                var response = await _repository.UpdatePublHabi(id, publhabi);
                if (response)
                    return Ok("Actualizado correctamente.");
                return BadRequest("Error al actualizar PublHabi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar PublHabi
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePublHabi(int id)
        {
            try
            {
                var response = await _repository.DeletePublHabi(id);
                if (response)
                    return Ok("Eliminado correctamente.");
                return BadRequest("Error al eliminar PublHabi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
