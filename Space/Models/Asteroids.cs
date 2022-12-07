#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class Asteroids
    {
        [DisplayName("Астероид")]
        public int AstId { get; set; }

        [DisplayName("Звезда")]
        public int? StarId { get; set; }

        [Required(ErrorMessage = "Не указано название астероида")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string AstName { get; set; }

        [Range(0.000001, int.MaxValue, ErrorMessage = "Диаметр должен быть положительным ")]
        [DisplayName("Диаметр (км)")]
        public int? AstDiameter { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Эксцентриситет должен быть положителен")]
        [DisplayName("Эксцентриситет")]
        public double? AstOrbitalEccentricity { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Наклонение должно быть положительно")]
        [DisplayName("Наклонение (°)")]
        public double? AstOrbitalInclination { get; set; }

        [DisplayName("Аргумент перицентра (°)")]
        public double? AstArgumentOfPerihelion { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Средняя аномалия должна быть положительна")]
        [DisplayName("Средняя аномалия (°)")]
        public double? AstMeanAnomaly { get; set; }

        public string AstImage { get; set; }    

        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}