#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Stars
    {
        public Stars()
        {
            Asteroids = new HashSet<Asteroids>();
            Comets = new HashSet<Comets>();
            Planets = new HashSet<Planets>();
        }

        [DisplayName("Звезда")]
        public int StarId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }
        [DisplayName("Звёздное скопление")]
        public int? StarclusterId { get; set; }
        [DisplayName("Планетная система")]
        public int? PlanetsystemId { get; set; }
        [DisplayName("Название")]
        public string StarName { get; set; }
        [DisplayName("Видимая звёздная величина")]
        public double? StarApparentMagnitude { get; set; }
        [DisplayName("Спектральный класс")]
        public string StarStellarClass { get; set; }
        [DisplayName("Расстояние (св.лет)")]
        public double? StarDistance { get; set; }
        [DisplayName("Склонение")]
        public string StarDeclination { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
        [DisplayName("Планетная система")]
        public virtual PlanetarySystems Planetsystem { get; set; }
        [DisplayName("Звёздное скопление")]
        public virtual StarClusters Starcluster { get; set; }
        public virtual ICollection<Asteroids> Asteroids { get; set; }
        public virtual ICollection<Comets> Comets { get; set; }
        public virtual ICollection<Planets> Planets { get; set; }
    }
}