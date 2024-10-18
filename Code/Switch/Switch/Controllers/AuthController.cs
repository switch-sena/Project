using SwitchBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SwitchBack.Repositories.Interfaces;

namespace SwitchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuariosRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IUsuariosRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Invalid client request");
            }

            var usuario = await _usuarioRepository.GetUsuarioByEmail(login.Email);
            if (usuario == null)
                return BadRequest();

            // Verificar las credenciales
            if (login.Email == usuario.CorreoUsua && login.Password == usuario.ClaveUsua)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, login.Email), // Usamos el email como nombre de usuario
                    new Claim(ClaimTypes.Role, "Admin") // Puedes ajustar el rol según sea necesario
                    },
                    expires: DateTime.Now.AddMinutes(35),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}