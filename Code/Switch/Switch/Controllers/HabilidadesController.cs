using Microsoft.AspNetCore.Mvc;
using Switch.Repositories.Interfaces;
using Switch.Models;

namespace Switch.Controllers
{
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
        public async Task<IActionResult> GetHabilidades()
        {
            var habilidades = await _repository.GetHabilidades();
            return Ok(habilidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabilidad(int id)
        {
            var habilidad = await _repository.GetHabilidadById(id);
            if (habilidad == null) return NotFound();
            return Ok(habilidad);
        }

        [HttpPost]
        public async Task<IActionResult> PostHabilidad([FromBody] Habilidades habilidad)
        {
            var result = await _repository.AddHabilidad(habilidad);
            if (result) return CreatedAtAction(nameof(GetHabilidad), new { id = habilidad.IdHabi }, habilidad);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidad(int id, [FromBody] Habilidades habilidad)
        {
            if (id != habilidad.IdHabi) return BadRequest();
            var result = await _repository.UpdateHabilidad(habilidad);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            var result = await _repository.DeleteHabilidad(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}
