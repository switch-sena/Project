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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPublModa()
        {
            var response = await _repository.GetPublModa();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPublModaById(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var publmoda = await _repository.GetPublModaById(id);
            if (publmoda == null) return NotFound();
            return Ok(publmoda);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublModa([FromBody] PublModa publmoda)
        {
            try
            {
                var response = await _repository.PostPublModa(publmoda);
                if (response == true)
                    return Ok("Insertado correctamente");
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePublModa(int id, [FromBody] PublModa publmoda)
        {
            try
            {
                var response = await _repository.UpdatePublModa(id, publmoda);
                if (response)
                    return Ok("Actualizado correctamente");
                else
                    return BadRequest("Error al actualizar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePublModa(int id)
        {
            try
            {
                var response = await _repository.DeletePublModa(id);
                if (response)
                    return Ok("Eliminado correctamente");
                else
                    return BadRequest("Error al eliminar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
