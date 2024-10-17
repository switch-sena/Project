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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHabilidades()
        {
            var habilidades = await _repository.GetHabilidades();
            return Ok(habilidades);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHabilidadesById(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var habilidad = await _repository.GetHabilidadesById(id);
            if (habilidad == null) return NotFound();
            return Ok(habilidad);
        }

        [HttpPost]
        public async Task<IActionResult> PostHabilidades([FromBody] Habilidades habilidad)
        {
            //preguntar a duveimar como funciona
            var result = await _repository.PostHabilidades(habilidad);
            if (result) return CreatedAtAction(nameof(GetHabilidades), new { id = habilidad.IdHabi }, habilidad);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabilidades(int id, [FromBody] Habilidades habilidad)
        {
            //preguntar a duveimar como funciona
            if (id != habilidad.IdHabi) return BadRequest();
            var result = await _repository.UpdateHabilidades(habilidad);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidades(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var result = await _repository.DeleteHabilidades(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}
