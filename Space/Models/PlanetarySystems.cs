#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class PlanetarySystems
    {
        public PlanetarySystems()
        {
            Stars = new HashSet<Stars>();
        }

        [DisplayName("Планетная система")]
        public int PlanetsystemId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }

        [Required(ErrorMessage = "Не указано название планетной системы")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string PlanetsystemName { get; set; }

        [Required(ErrorMessage = "Не указано количество подтверждённых планет")]
        [Range(0.000001, byte.MaxValue, ErrorMessage = "Количество подтверждённых планет должно быть положительно")]
        [DisplayName("Подтверждённых планет")]
        public byte PlanetsystemConfirmedPlanets { get; set; }

        public string PlanetsystemImage { get; set; }


        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }
    }
}