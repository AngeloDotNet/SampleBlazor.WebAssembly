using DemoBlazorApp.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazorApp.Server.Models.Services.Application.Persone
{
    public class EfCorePersonaService : IPersonaService
    {
        private readonly ILogger<EfCorePersonaService> logger;
        private readonly BlazorAppDbContext dbContext;

        public EfCorePersonaService(ILogger<EfCorePersonaService> logger, BlazorAppDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<List<PersonaEntity>> ElencoPersone()
        {
            return await dbContext.Persone.ToListAsync();
        }

        public async Task AggiungiPersona(PersonaEntity persona)
        {
            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PersonaEntity> DatiPersona(int id)
        {
            return await dbContext.Persone.FindAsync(id);
        }

        public async Task ModificaPersona(PersonaEntity persona)
        {
            var personaOld = await dbContext.Persone.FindAsync(persona.PersonaId);
            if (personaOld == null)
                return;

            personaOld.Telefono = persona.Telefono;
            personaOld.Cognome = persona.Cognome;
            personaOld.Email = persona.Email;
            personaOld.Nome = persona.Nome;
            await dbContext.SaveChangesAsync();
        }

        public async Task CancellaPersona(int id)
        {
            var personaOld = await dbContext.Persone.FindAsync(id);
            if (personaOld == null)
                return;

            dbContext.Persone.Remove(personaOld);
            await dbContext.SaveChangesAsync();
        }
    }
}