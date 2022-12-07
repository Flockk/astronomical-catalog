#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Space.Models
{
    public partial class BlackHoles
    {
        [DisplayName("Чёрная дыра")]
        public int BlackHoleId { get; set; }
        [DisplayName("Созвездие")]
        public int ConsId { get; set; }
        [DisplayName("Галактика")]
        public int? GlxId { get; set; }

        [Required(ErrorMessage = "Не указано название чёрной дыры")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string BlackholeName { get; set; }

        [StringLength(14, ErrorMessage = "Длина строки должна быть до 14 символов")]
        [DisplayName("Тип")]
        public string BlackholeType { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Прямое восхождение")]
        public TimeSpan? BlackholeRightAscension { get; set; }

        [StringLength(20, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [DisplayName("Склонение")]
        public string BlackholeDeclination { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Расстояние должно быть положительно")]
        [DisplayName("Расстояние (кпк)")]
        public double? BlackholeDistance { get; set; }

        public string BlackholeImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
    }
}