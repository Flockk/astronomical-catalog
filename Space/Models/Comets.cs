#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Comets
    {
        [DisplayName("Комета")]
        public int CometId { get; set; }
        [DisplayName("Звезда")]
        public int? StarId { get; set; }
        [DisplayName("Название")]
        public string CometName { get; set; }
        [DisplayName("Период обращения (лет)")]
        public double? CometOrbitalPeriod { get; set; }
        [DisplayName("Большая полуось (а.е.)")]
        public double? CometSemiMajorAxis { get; set; }
        [DisplayName("Перигелий (а.е.)")]
        public double? CometPerihelion { get; set; }
        [DisplayName("Эксцентриситет")]
        public double? CometEccentricity { get; set; }
        [DisplayName("Наклонение орбиты (°)")]
        public double? CometOrbitalInclination { get; set; }

        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}