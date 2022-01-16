using DemoBlazorApp.Shared.Models.Entities;
using DemoBlazorApp.Server.Models.Services.Infrastructure;
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

        public async Task<List<Persona>> ElencoPersone()
        {
            return await dbContext.Persone.ToListAsync();
        }

        public async Task AggiungiPersona(Persona persona)
        {
            //persona.PersonaId = Guid.NewGuid().ToString(); In questo caso non serve in quanto l'ID viene generato automaticamente da SQLite
            dbContext.Add(persona);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Persona> DatiPersona(int id)
        {
            return await dbContext.Persone.FindAsync(id);
        }

        public async Task<bool> ModificaPersona(int id, Persona persona)
        {
            if (id != persona.PersonaId)
            {
                return false;
            }

            dbContext.Entry(persona).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancellaPersona(int id)
        {
            var personaId = await dbContext.Persone.FindAsync(id);
            
            if (personaId == null)
            {
                return false;
            }

            dbContext.Persone.Remove(personaId);
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}