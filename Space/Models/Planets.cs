#nullable disable
using System.ComponentModel;

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
        [DisplayName("Название")]
        public string PlntName { get; set; }
        [DisplayName("Эксцентриситет")]
        public double? PlntEccentricity { get; set; }
        [DisplayName("Большая полуось (а.е.)")]
        public double? PlntSemiMajorAxis { get; set; }
        [DisplayName("Период обращения (лет)")]
        public double? PlntOrbitalPeriod { get; set; }
        [DisplayName("Аргумент перицентра (°)")]
        public double? PlntArgumentOfPerihelion { get; set; }
        [DisplayName("Масса (M\x2097)")]
        public double? PlntMass { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}