using Switch.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Switch.Models;
using Microsoft.AspNetCore.Authorization;

namespace Switch.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _repository;

        public UsuariosController(IUsuariosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _repository.GetUsuarios();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _repository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUsuario([FromBody] Usuarios usuario)
        {
            var created = await _repository.PostUsuario(usuario);
            if (created)
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.IdUsua }, usuario);
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuarios usuario)
        {
            if (id != usuario.IdUsua)
                return BadRequest();

            var updated = await _repository.UpdateUsuario(usuario);
            if (updated)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await _repository.DeleteUsuario(id);
            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}