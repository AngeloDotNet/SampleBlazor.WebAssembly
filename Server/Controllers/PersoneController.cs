using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoBlazorApp.Shared.Models.Entities;
using DemoBlazorApp.Shared.Models.Services.Application.Persone;

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
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersone()
        {
            return await personaService.ElencoPersone();
        }

        // GET: api/Persone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await personaService.DatiPersona(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Persone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.PersonaId)
            {
                return BadRequest();
            }

            await personaService.ModificaPersona(id, persona);

            return NoContent();
        }

        // POST: api/Persone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            await personaService.AggiungiPersona(persona);

            return CreatedAtAction("GetPersona", new { id = persona.PersonaId }, persona);
        }

        // DELETE: api/Persone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await personaService.CancellaPersona(id);

            return NoContent();
        }
    }
}
