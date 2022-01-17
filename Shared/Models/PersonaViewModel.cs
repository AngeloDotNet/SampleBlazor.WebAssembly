using System.ComponentModel.DataAnnotations;

namespace DemoBlazorApp.Shared.Models
{
    public class PersonaViewModel
    {
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "L'indirizzo email è obbligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il telefono è obbligatorio")]
        public string Telefono { get; set; }
    }
}