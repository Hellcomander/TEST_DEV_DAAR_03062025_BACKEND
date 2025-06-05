using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST_DEV_DAAR_03062025.Data;
using TEST_DEV_DAAR_03062025.Dto;
using TEST_DEV_DAAR_03062025.Models;
using TEST_DEV_DAAR_03062025.Utils;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using TEST_DEV_DAAR_03062025.Services;
using Microsoft.AspNetCore.Authorization;

namespace TEST_DEV_DAAR_03062025.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController(ApplicationDbContext context, IConfiguration configuration, JwtService jwtService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtService _jwtService = jwtService;

        private async Task<ActionResult<User>> GetUsuario(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || !user.Activo)
                return NotFound();

            return user;
        }

        // POST: user
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserCreateDto usuario)
        {
            var existeUsuario = await _context.Users.AnyAsync(u => u.Correo == usuario.Correo);

            if (existeUsuario) {
                return Conflict(new { message = "Ya existe un usuario con ese Correo." });
            }

            var nuevoUsuario = new User
            {
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                Password = PasswordUtils.Hash(usuario.Password)
            };

            _context.Users.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Created();
        }

        // POST: user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto authData)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Correo == authData.Correo && u.Activo);

            if (user == null || !PasswordUtils.Verify(user?.Password ?? "", authData.Password))
                return Unauthorized("Usuario o contraseña incorrectos!");

            var httpClient = new HttpClient();

            string json = JsonSerializer.Serialize(new
            {
                Username = _configuration["ApiToka:Username"],
                Password = _configuration["ApiToka:Password"]
            });

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(_configuration["ApiToka:Url"], httpContent);

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(jsonResponse);
            var tokenToka = jsonDoc.RootElement.GetProperty("Data").GetString();

            var jwt = _jwtService.GenerateToken(user);

            return Ok(new { tokenToka,  accessToken = jwt});
        }
    }
}