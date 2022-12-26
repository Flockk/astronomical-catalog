#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class Nebulae
    {
        [DisplayName("Туманность")]
        public int NebulaId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }

        [Remote("IsNebulaNameExist", "Nebulae", ErrorMessage = "Данное название туманности уже существует!")]
        [Required(ErrorMessage = "Не указано название туманности")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string NebulaName { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Тип")]
        public string NebulaType { get; set; }

        [DisplayName("Прямое восхождение")]
        public TimeSpan? NebulaRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string NebulaDeclination { get; set; }

        [Range(0.000001, int.MaxValue, ErrorMessage = "Расстояние должно быть положительно")]
        [DisplayName("Расстояние")]
        public int? NebulaDistance { get; set; }

        public string NebulaImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
    }
}