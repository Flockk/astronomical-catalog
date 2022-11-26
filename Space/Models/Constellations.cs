#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Constellations
    {
        public Constellations()
        {
            BlackHoles = new HashSet<BlackHoles>();
            Galaxies = new HashSet<Galaxies>();
            GalaxyClusters = new HashSet<GalaxyClusters>();
            GalaxyGroups = new HashSet<GalaxyGroups>();
            Nebulae = new HashSet<Nebulae>();
            PlanetarySystems = new HashSet<PlanetarySystems>();
            Planets = new HashSet<Planets>();
            StarClusters = new HashSet<StarClusters>();
            Stars = new HashSet<Stars>(); 
        }

        [DisplayName("Созвездие")]
        public int ConsId { get; set; }
        [DisplayName("Название")]
        public string ConsName { get; set; }
        [DisplayName("Сокращение")]
        public string ConsAbbreviation { get; set; }
        [DisplayName("Символ")]
        public string ConsSymbolism { get; set; }
        [DisplayName("Прямое восхождение")]
        public string ConsRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string ConsDeclination { get; set; }
        [DisplayName("Площадь (кв. °)")]
        public int? ConsSquare { get; set; }
        [DisplayName("Видно в широтах")]
        public string ConsVisibleInLatitudes { get; set; }

        public string ConsImage { get; set; }

        public virtual ICollection<BlackHoles> BlackHoles { get; set; }
        public virtual ICollection<Galaxies> Galaxies { get; set; }
        public virtual ICollection<GalaxyClusters> GalaxyClusters { get; set; }
        public virtual ICollection<GalaxyGroups> GalaxyGroups { get; set; }
        public virtual ICollection<Nebulae> Nebulae { get; set; }
        public virtual ICollection<PlanetarySystems> PlanetarySystems { get; set; }
        public virtual ICollection<Planets> Planets { get; set; }
        public virtual ICollection<StarClusters> StarClusters { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }
    }
}