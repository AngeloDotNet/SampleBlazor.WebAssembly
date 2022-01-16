using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoBlazorApp.Shared.Models.Entities;
using DemoBlazorApp.Server.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersoneController : ControllerBase
    {
        //TODO: E' possibile cancellare la parte APPLICATION / PERSONE in quanto non viene più utilizzata
        private readonly BlazorAppDbContext dbContext;
        //private readonly IPersonaService personaService;

        //public PersoneController(IPersonaService personaService, BlazorAppDbContext dbContext)
        public PersoneController(BlazorAppDbContext dbContext)
        {
            //this.personaService = personaService;
            this.dbContext = dbContext;
        }

        // GET: api/Persone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersone()
        {
            //return await personaService.ElencoPersone();
            return await dbContext.Persone.ToListAsync();
        }

        // POST: api/Persone
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            //await personaService.AggiungiPersona(persona);
            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.PersonaId }, persona);
        }

        // GET: api/Persone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            //var persona = await personaService.DatiPersona(id);
            var persona = await dbContext.Persone.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Persone/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(Persona persona)
        //public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            // if (id != persona.PersonaId)
            // {
            //     return BadRequest();
            // }

            // await personaService.ModificaPersona(id, persona);
            // return NoContent();

            var Id = await dbContext.Persone.FindAsync(persona.PersonaId);
            
            if (Id == null)
            {
                return BadRequest();
            }

            // if (id != persona.PersonaId)
            // {
            //     return BadRequest();
            // }

            dbContext.Entry(persona).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Persone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            //await personaService.CancellaPersona(id);

            var personaId = await dbContext.Persone.FindAsync(id);
            
            if (personaId == null)
            {
                return BadRequest();
            }

            dbContext.Persone.Remove(personaId);
            await dbContext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
