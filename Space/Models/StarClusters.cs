#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class StarClusters
    {
        public StarClusters()
        {
            Stars = new HashSet<Stars>();
        }

        public int StarclusterId { get; set; }
        public int? ConsId { get; set; }
        public int? GlxId { get; set; }
        [DisplayName("Название")]
        public string StarclusterName { get; set; }
        [DisplayName("Тип")]
        public string StarclusterType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? StarclusterRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string StarclusterDeclination { get; set; }
        [DisplayName("Расстояние (пк)")]
        public double? StarclusterDistance { get; set; }
        [DisplayName("Возраст (млн.лет)")]
        public int? StarclusterAge { get; set; }
        [DisplayName("Диаметр (‘)")]
        public double? StarclusterDiameter { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        [DisplayName("Галактика")]
        public virtual Galaxies Glx { get; set; }
        public virtual ICollection<Stars> Stars { get; set; }
    }
}