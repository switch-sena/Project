using Switch.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Switch.Models;
using Microsoft.AspNetCore.Authorization;

namespace Switch.Controllers
{
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostPublModa([FromBody] PublModa publModa)
        {
            try
            {
                var response = await _repository.PostPublModa(publModa);
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
        public async Task<IActionResult> UpdatePublModa(int id, [FromBody] PublModa publModa)
        {
            try
            {
                var response = await _repository.UpdatePublModa(id, publModa);
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
