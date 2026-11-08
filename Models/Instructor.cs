using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "4|El nombre del monitor es obligatorio.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "4|El email del monitor es obligatorio.")]
        [EmailAddress(ErrorMessage = "4|El email del monitor no tiene un formato válido.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // La foto es una URL. En la API será obligatoria, pero en la BBDD puede ser opcional.
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }

        // El teléfono es obligatorio en la API, pero puede ser opcional en la BBDD.
        [MaxLength(20)]
        public string? Phone { get; set; }

        // Propiedad de navegación (M:N) Monitor -> Actividades
        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
