#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Remote("IsConsNameExist", "Constellations", ErrorMessage = "Данное название созвездия уже существует!")]
        [Required(ErrorMessage = "Не указано название созвездия")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string ConsName { get; set; }

        [Remote("IsConsAbbreviationExist", "Constellations", ErrorMessage = "Данное сокращение созвездия уже существует!")]
        [Required(ErrorMessage = "Не указано сокращение созвездия")]
        [StringLength(3, ErrorMessage = "Длина строки должна быть до 3 символов")]
        [DisplayName("Сокращение")]
        public string ConsAbbreviation { get; set; }

        [Remote("IsConsSymbolismExist", "Constellations", ErrorMessage = "Данный символ созвездия уже существует!")]
        [Required(ErrorMessage = "Не указан символ созвездия")]
        [StringLength(22, ErrorMessage = "Длина строки должна быть до 22 символов")]
        [DisplayName("Символ")]
        public string ConsSymbolism { get; set; }

        [StringLength(17, ErrorMessage = "Длина строки должна быть до 17 символов")]
        [DisplayName("Прямое восхождение")]
        public string ConsRightAscension { get; set; }

        [StringLength(15, ErrorMessage = "Длина строки должна быть до 15 символов")]
        [DisplayName("Склонение")]
        public string ConsDeclination { get; set; }

        [Range(0.000001, int.MaxValue, ErrorMessage = "Площадь должна быть положительна")]
        [DisplayName("Площадь (кв.°)")]
        public int? ConsSquare { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
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