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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetModalidades()
        {
            var response = await _repository.GetModalidades();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetModalidadesById(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var modalidades = await _repository.GetModalidadesById(id);
            if (modalidades == null) return NotFound();
            return Ok(modalidades);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostModalidades([FromBody] Modalidades modalidades)
        {
            try
            {
                var response = await _repository.PostModalidades(modalidades);
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
        public async Task<IActionResult> UpdateModalidades(int id, [FromBody] Modalidades modalidades)
        {
            //preguntar a duveimar como funciona
            if (id != modalidades.IdModa) return BadRequest();
            var result = await _repository.UpdateModalidades(modalidades);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModalidades(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var result = await _repository.DeleteModalidades(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}
