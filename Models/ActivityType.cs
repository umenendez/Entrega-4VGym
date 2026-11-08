using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "4|El nombre del tipo de actividad es obligatorio.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 25, ErrorMessage = "5|El número mínimo de monitores debe estar entre 1 y 25.")]
        public int NumberMonitors { get; set; } = 1;

        // Propiedad de navegación inversa (1 Tipo de actividad -> Muchas Actividades)
        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
