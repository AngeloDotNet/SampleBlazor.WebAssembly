using DemoBlazorApp.Server.Entities;
using DemoBlazorApp.Server.Models.Services.Application.Persone;
using DemoBlazorApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersoneController : ControllerBase
    {
        private readonly IPersonaService personaService;

        public PersoneController(IPersonaService personaService)
        {
            this.personaService = personaService;
        }

        // GET: api/Persone
        [HttpGet]
        public async Task<IActionResult> GetPersone()
        {
            var entities = await personaService.ElencoPersone();

            return Ok(entities.Select(t => new PersonaViewModel
            {
                Nome = t.Nome,
                Cognome = t.Cognome,
                Email = t.Email,
                Telefono = t.Telefono,
                PersonaId = t.PersonaId,
            }));
        }

        // POST: api/Persone
        [HttpPost]
        public async Task<ActionResult<PersonaViewModel>> PostPersona(PersonaViewModel persona)
        {
            var entity = new PersonaEntity
            {
                Nome = persona.Nome,
                Email = persona.Email,
                Cognome = persona.Cognome,
                Telefono = persona.Telefono
            };
            await personaService.AggiungiPersona(entity);
            return CreatedAtAction("GetPersona", new { id = persona.PersonaId }, persona);
        }

        // GET: api/Persone/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var persona = await personaService.DatiPersona(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Persone/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, PersonaViewModel persona)
        {
            if (id != persona.PersonaId)
            {
                return BadRequest();
            }
            var entity = new PersonaEntity
            {
                PersonaId = persona.PersonaId,
                Nome = persona.Nome,
                Email = persona.Email,
                Cognome = persona.Cognome,
                Telefono = persona.Telefono
            };
            await personaService.ModificaPersona(entity);
            return Ok();
        }

        // DELETE: api/Persone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await personaService.CancellaPersona(id);
            return Ok();
        }
    }
}
