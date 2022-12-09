#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class StarClusters
    {
        public StarClusters()
        {
            Stars = new HashSet<Stars>();
        }

        public int StarclusterId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }

        [Remote("IsStarclusterNameExist", "StarClusters", ErrorMessage = "Данное название звёздного скопления уже существует!")]
        [Required(ErrorMessage = "Не указано название звёздного скопления")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string StarclusterName { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Тип")]
        public string StarclusterType { get; set; }

        [DisplayName("Прямое восхождение")]
        public TimeSpan? StarclusterRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string StarclusterDeclination { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Расстояние должно быть положительно")]
        [DisplayName("Расстояние (пк)")]
        public double? StarclusterDistance { get; set; }

        [Range(0.000001, int.MaxValue, ErrorMessage = "Возраст должен быть положителен")]
        [DisplayName("Возраст (млн.лет)")]
        public int? StarclusterAge { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Диаметр должен быть положителен")]
        [DisplayName("Диаметр (‘)")]
        public double? StarclusterDiameter { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }

        public string StarclusterImage { get; set; }

        public virtual ICollection<Stars> Stars { get; set; }
    }
}