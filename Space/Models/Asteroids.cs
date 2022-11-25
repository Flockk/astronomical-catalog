#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class Asteroids
    {
        [DisplayName("Астероид")]
        public int AstId { get; set; }
        [DisplayName("Звезда")]
        public int? StarId { get; set; }
        [DisplayName("Название")]
        public string AstName { get; set; }
        [DisplayName("Диаметр (км)")]
        public int? AstDiameter { get; set; }
        [DisplayName("Эксцентриситет")]
        public double? AstOrbitalEccentricity { get; set; }
        [DisplayName("Наклонение (°)")]
        public double? AstOrbitalInclination { get; set; }
        [DisplayName("Аргумент перицентра (°)")]
        public double? AstArgumentOfPerihelion { get; set; }
        [DisplayName("Средняя аномалия (°)")]
        public double? AstMeanAnomaly { get; set; }

        [DisplayName("Звезда")]
        public virtual Stars Star { get; set; }
    }
}