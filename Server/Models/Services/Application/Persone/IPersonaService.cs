using DemoBlazorApp.Shared.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBlazorApp.Server.Models.Services.Application.Persone
{
    public interface IPersonaService
    {
        Task<List<Persona>> ElencoPersone();
        //Task<bool> AggiungiPersona(Persona persona);
        Task AggiungiPersona(Persona persona);
        Task<bool> ModificaPersona(int id, Persona persona);
        Task<Persona> DatiPersona(int id);
        Task<bool> CancellaPersona(int id);
    }
}