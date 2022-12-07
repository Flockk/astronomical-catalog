#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space.Models
{
    public partial class Planets
    {
        [DisplayName("Планета")]
        public int PlntId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Звезда")]
        public int? StarId { get; set; }

        [Required(ErrorMessage = "Не указано название планеты")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        [DisplayName("Название")]
        public string PlntName { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Эксцентриситет должно быть положительно")]
        [DisplayName("Эксцентриситет")]
        public double? PlntEccentricity { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Большая полуось должна быть положительна")]
        [DisplayName("Большая полуось (а.е.)")]
        public double? PlntSemiMajorAxis { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Орбитальный период должен быть положителен")]
        [DisplayName("Период обращения (дней)")]
        public double? PlntOrbitalPeriod { get; set; }

        [DisplayName("Аргумент перицентра (°)")]
        public double? PlntArgumentOfPerihelion { get; set; }

        [Range(0.000001, double.MaxValue, ErrorMessage = "Масса должна быть положительна")]
        [DisplayName("Масса (M\x2097)")]
        public double? PlntMass { get; set; }

        public string PlntImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}