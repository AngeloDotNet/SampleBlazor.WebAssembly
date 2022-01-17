using DemoBlazorApp.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazorApp.Server.Models.Services.Application.Persone
{
    public interface IPersonaService
    {
        Task<List<PersonaEntity>> ElencoPersone();
        Task AggiungiPersona(PersonaEntity persona);
        Task ModificaPersona(PersonaEntity persona);
        Task<PersonaEntity> DatiPersona(int id);
        Task CancellaPersona(int id);
    }
}