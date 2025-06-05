using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST_DEV_DAAR_03062025.Data;
using TEST_DEV_DAAR_03062025.Dto;
using TEST_DEV_DAAR_03062025.Models;
using TEST_DEV_DAAR_03062025.Services;
using Microsoft.AspNetCore.Authorization;

namespace TEST_DEV_DAAR_03062025.Controllers
{
    [ApiController]
    [Route("persona-fisica")]
    public class PersonaFisicaController(ApplicationDbContext context, PersonaService _personaService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        // GET: persona-fisica
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaFisica>>> GetPersonas()
        {
            return await _context.PersonasFisicas.Where(p => p.Activo).ToListAsync();
        }

        // GET: persona-fisica/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaFisica>> GetPersona(int id)
        {
            var persona = await _context.PersonasFisicas.FindAsync(id);

            if (persona == null || !persona.Activo)
                return NotFound();

            return persona;
        }

        // POST: persona-fisica
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PersonaFisica>> PostPersona([FromBody] PersonaFisicaCreateDto persona)
        {

            if (await _personaService.ExistePersonaConRFC(persona.RFC)) {
                return Conflict(new { message = "Ya existe una persona con ese RFC." });
            }   
                            
            var nuevaPersona = new PersonaFisica
            {
                Nombre = persona.Nombre,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                RFC = persona.RFC,
                FechaNacimiento = persona.FechaNacimiento,
                UsuarioAgrega = persona.UsuarioAgrega,
            };

            _context.PersonasFisicas.Add(nuevaPersona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = nuevaPersona.IdPersonaFisica }, new {
                idPersonaFisica = nuevaPersona.IdPersonaFisica,
                nombre = nuevaPersona.Nombre,
                apellidoPaterno = nuevaPersona.ApellidoPaterno,
                apellidoMaterno = nuevaPersona.ApellidoMaterno,
                rfc = nuevaPersona.RFC,
                fechaNacimiento = nuevaPersona.FechaNacimiento,
            });
        }

        // PUT: persona-fisica/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, PersonaFisicaUpdateDto persona)
        {
            if (id != persona.IdPersonaFisica)
                return BadRequest();

            var personaExistente = await _context.PersonasFisicas.FindAsync(id);

            if (personaExistente == null || !personaExistente.Activo)
                return NotFound();

            if (personaExistente.RFC != persona.RFC) {
                if (await _personaService.ExistePersonaConRFC(persona.RFC))
                {
                    return Conflict(new { message = "Ya existe una persona con ese RFC." });
                }                   
            }

            // Actualizar campos permitidos
            personaExistente.Nombre = persona.Nombre;
            personaExistente.ApellidoPaterno = persona.ApellidoPaterno;
            personaExistente.ApellidoMaterno = persona.ApellidoMaterno;
            personaExistente.RFC = persona.RFC;
            personaExistente.FechaNacimiento = persona.FechaNacimiento;
            personaExistente.FechaActualizacion = persona.FechaActualizacion;

            _context.Entry(personaExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PersonasFisicas.Any(e => e.IdPersonaFisica == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: persona-fisica/5 (eliminación lógica)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.PersonasFisicas.FindAsync(id);
            if (persona == null || !persona.Activo)
                return NotFound();

            // Eliminación lógica
            persona.Activo = false;
            persona.FechaActualizacion = DateTime.UtcNow;

            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
