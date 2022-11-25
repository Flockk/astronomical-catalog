#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Galaxies
    {
        public Galaxies()
        {
            BlackHoles = new HashSet<BlackHoles>();
            Nebulae = new HashSet<Nebulae>();
            PlanetarySystems = new HashSet<PlanetarySystems>();
            StarClusters = new HashSet<StarClusters>();
            Stars = new HashSet<Stars>();
        }


        [DisplayName("Галактика")]
        public int GlxId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Звёздное скопление")]
        public int? GlxclusterId { get; set; }
        [DisplayName("Группа галактик")]
        public int? GlxgroupId { get; set; }
        [DisplayName("Название")]
        public string GlxName { get; set; }
        [DisplayName("Тип")]
        public string GlxType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string GlxDeclination { get; set; }
        [DisplayName("Красное смещение")]
        public double? GlxRedshift { get; set; }
        [DisplayName("Расстояние (кпк)")]
        public int? GlxDistance { get; set; }
        [DisplayName("Видимая звёздная величина")]
        public double? GlxApparentMagnitude { get; set; }
        [DisplayName("Лучевая скорость (км/с)")]
        public int? GlxRadialVelocity { get; set; }
        [DisplayName("Радиус (кпк)")]
        public double? GlxRadius { get; set; }

        [DisplayName("Созвевздие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Скопление галактик")]
        public virtual GalaxyClusters Glxcluster { get; set; }
        [DisplayName("Группа галактик")]
        public virtual GalaxyGroups Glxgroup { get; set; }
        public virtual ICollection<BlackHoles> BlackHoles { get; set; }
        public virtual ICollection<Nebulae> Nebulae { get; set; }
        public virtual ICollection<PlanetarySystems> PlanetarySystems { get; set; }
        public virtual ICollection<StarClusters> StarClusters { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }
    }
}