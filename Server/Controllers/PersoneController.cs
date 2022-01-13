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
        //private readonly BlazorAppDbContext _context;
        private readonly IPersonaService personaService;

        public PersoneController(IPersonaService personaService)
        {
            //_context = context;
            this.personaService = personaService;
        }

        // GET: api/Persone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersone()
        {
            //return await _context.Persone.ToListAsync();
            return await personaService.ElencoPersone();
        }

        // GET: api/Persone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            //var persona = await _context.Persone.FindAsync(id);
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

            //_context.Entry(persona).State = EntityState.Modified;
            await personaService.ModificaPersona(id, persona);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PersonaExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Persone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            //_context.Persone.Add(persona);
            //await _context.SaveChangesAsync();
            await personaService.AggiungiPersona(persona);

            return CreatedAtAction("GetPersona", new { id = persona.PersonaId }, persona);
        }

        // DELETE: api/Persone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            //var persona = await _context.Persone.FindAsync(id);
            //if (persona == null)
            //{
            //    return NotFound();
            //}

            //_context.Persone.Remove(persona);
            //await _context.SaveChangesAsync();

            await personaService.CancellaPersona(id);

            return NoContent();
        }

        //private bool PersonaExists(int id)
        //{
        //    return _context.Persone.Any(e => e.PersonaId == id);
        //}
    }
}
