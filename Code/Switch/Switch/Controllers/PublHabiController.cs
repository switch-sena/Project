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

        // Crear nuevo PublHabi
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublHabi([FromBody] PublHabi publHabi)
        {
            try
            {
                var response = await _repository.PostPublHabi(publHabi);
                if (response)
                    return CreatedAtAction(nameof(GetPublHabi), new { id = publHabi.Id }, publHabi);
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
        public async Task<IActionResult> UpdatePublHabi(int id, [FromBody] PublHabi publHabi)
        {
            try
            {
                var response = await _repository.UpdatePublHabi(id, publHabi);
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
