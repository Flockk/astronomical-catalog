#nullable disable
using System.ComponentModel;

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
        [DisplayName("Название")]
        public string PlanetsystemName { get; set; }
        [DisplayName("Подтверждённых планет")]
        public byte PlanetsystemConfirmedPlanets { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }
    }
}