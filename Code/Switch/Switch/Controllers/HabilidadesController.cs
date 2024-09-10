﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetHabilidad(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var habilidad = await _repository.GetHabilidadById(id);
            if (habilidad == null) return NotFound();
            return Ok(habilidad);
        }

        [HttpPost]
        public async Task<IActionResult> PostHabilidad([FromBody] Habilidades habilidad)
        {
            //preguntar a duveimar como funciona
            var result = await _repository.PostHabilidad(habilidad);
            if (result) return CreatedAtAction(nameof(GetHabilidad), new { id = habilidad.IdHabi }, habilidad);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidad(int id, [FromBody] Habilidades habilidad)
        {
            //preguntar a duveimar como funciona
            if (id != habilidad.IdHabi) return BadRequest();
            var result = await _repository.UpdateHabilidad(habilidad);
            if (result) return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            //preguntar al profesor como se programan los response pero la parte del estado (200, 404,...)
            var result = await _repository.DeleteHabilidad(id);
            if (result) return NoContent();
            return NotFound();
        }
    }
}
