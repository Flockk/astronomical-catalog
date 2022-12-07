#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space.Models
{
    public partial class Comets
    {
        [DisplayName("Комета")]
        public int CometId { get; set; }
        [DisplayName("Звезда")]
        public int? StarId { get; set; }

        [Required(ErrorMessage = "Не указано название кометы")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string CometName { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Период обращения должен быть положителен")]
        [DisplayName("Период обращения (лет)")]
        public double? CometOrbitalPeriod { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Большая полуось должна быть положительна")]
        [DisplayName("Большая полуось (а.е.)")]
        public double? CometSemiMajorAxis { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Перигелий должен быть положителен")]
        [DisplayName("Перигелий (а.е.)")]
        public double? CometPerihelion { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Эксцентриситет должен быть положителен")]
        [DisplayName("Эксцентриситет")]
        public double? CometEccentricity { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Наклонение должно быть положительно")]
        [DisplayName("Наклонение орбиты (°)")]
        public double? CometOrbitalInclination { get; set; }

        public string CometImage { get; set; }

        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}