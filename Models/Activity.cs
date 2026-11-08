using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class Activity
    {
        public int Id { get; set; }

        // CLAVE FORÁNEA: ActivityType (1 a N). Varias actividades -> 1 tipo de actividad.
        [Required(ErrorMessage = "30|El tipo de actividad es obligatorio.")]
        public int ActivityTypeId { get; set; }

        [ForeignKey(nameof(ActivityTypeId))]
        public ActivityType? ActivityType { get; set; }

        [Range(1, 25, ErrorMessage = "25|El aforo máximo debe estar entre 1 y 25 participantes.")]
        public int MaxParticipants { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        // Relación M:N. EF Core genera la tabla intermedia ActivityInstructor.
        [JsonIgnore]
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
