using System.ComponentModel.DataAnnotations;

namespace DemoBlazorApp.Shared.Models.Entities
{
    public partial class Persona
    {
        public int PersonaId { get; set; }
        
        [Required]
        public string Cognome { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Telefono { get; set; }
    }
}