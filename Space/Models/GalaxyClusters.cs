#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class GalaxyClusters
    {
        public GalaxyClusters()
        {
            Galaxies = new HashSet<Galaxies>();
        }

        [DisplayName("Скопление галактик")]
        public int GlxclusterId { get; set; }
        [DisplayName("Созвездие")]
        public int ConsId { get; set; }
        [DisplayName("Название")]
        public string GlxclusterName { get; set; }
        [DisplayName("Тип")]
        public string GlxclusterType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxclusterRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string GlxclusterDeclination { get; set; }
        [DisplayName("Красное смещение")]
        public double? GlxclusterRedshift { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }

		public string GlxclusterImage { get; set; }

		public virtual ICollection<Galaxies> Galaxies { get; set; }
    }
}