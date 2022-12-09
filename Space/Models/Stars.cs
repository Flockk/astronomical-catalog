#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Remote("IsStarNameExist", "Stars", ErrorMessage = "Данное название звезды уже существует!")]
        [Required(ErrorMessage = "Не указано название звезды")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string StarName { get; set; }

        [DisplayName("Видимая звёздная величина")]
        public double? StarApparentMagnitude { get; set; }

        [StringLength(10, ErrorMessage = "Длина строки должна быть до 10 символов")]
        [DisplayName("Спектральный класс")]
        public string StarStellarClass { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Расстояние должно быть положительно")]
        [DisplayName("Расстояние (св.лет)")]
        public double? StarDistance { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string StarDeclination { get; set; }

        public string StarImage { get; set; }

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