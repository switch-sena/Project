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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBarrios()
        {
            var response = await _repository.GetBarrios();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBarriosById(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var barrio = await _repository.GetBarriosById(id);
            if (barrio == null) return NotFound();
            return Ok(barrio);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostBarrios([FromBody] Barrios barrios)
        {
            try
            {
                var response = await _repository.PostBarrios(barrios);
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
        public async Task<IActionResult> UpdateBarrios(int id, [FromBody] Barrios barrios)
        {
            //preguntar a duveimar como funciona
            if (id != barrios.IdBarr) return BadRequest();
            var result = await _repository.UpdateBarrios(barrios);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarrios(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var result = await _repository.DeleteBarrios(id);
            if (result) return NoContent();
            return NotFound();
        }
    }

}
