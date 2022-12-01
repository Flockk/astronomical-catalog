#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Не указано название галактики")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string GlxName { get; set; }

        [StringLength(14, ErrorMessage = "Длина строки должна быть до 14 символов")]
        [DisplayName("Тип")]
        public string GlxType { get; set; }

        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string GlxDeclination { get; set; }

        [DisplayName("Красное смещение")]
        public double? GlxRedshift { get; set; }

        [Range(0.000001, int.MaxValue, ErrorMessage = "Расстояние должно быть положительно")]
        [DisplayName("Расстояние (кпк)")]
        public int? GlxDistance { get; set; }

        [DisplayName("Видимая звёздная величина")]
        public double? GlxApparentMagnitude { get; set; }

        [DisplayName("Лучевая скорость (км/с)")]
        public int? GlxRadialVelocity { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Радиус должен быть положителен")]
        [DisplayName("Радиус (кпк)")]
        public double? GlxRadius { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Скопление галактик")]
        public virtual GalaxyClusters Glxcluster { get; set; }
        [DisplayName("Группа галактик")]
        public virtual GalaxyGroups Glxgroup { get; set; }

		public string GlxImage { get; set; }


		public virtual ICollection<BlackHoles> BlackHoles { get; set; }
        public virtual ICollection<Nebulae> Nebulae { get; set; }
        public virtual ICollection<PlanetarySystems> PlanetarySystems { get; set; }
        public virtual ICollection<StarClusters> StarClusters { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }

    }
}