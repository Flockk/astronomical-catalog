#nullable disable
using System.ComponentModel;

namespace Space.Models
{
    public partial class GalaxyGroups
    {
        public GalaxyGroups()
        {
            Galaxies = new HashSet<Galaxies>();
        }

        [DisplayName("Группа галактик")]
        public int GlxgroupId { get; set; }
        [DisplayName("Созвездие")]
        public int? ConsId { get; set; }
        [DisplayName("Название")]
        public string GlxgroupName { get; set; }
        [DisplayName("Тип")]
        public string GlxgroupType { get; set; }
        [DisplayName("Прямое восхождение")]
        public TimeSpan? GlxgroupRightAscension { get; set; }
        [DisplayName("Склонение")]
        public string GlxgroupDeclination { get; set; }
        [DisplayName("Красное смещение")]
        public double? GlxgroupRedshift { get; set; }

        public string GlxgroupImage { get; set; }

        [DisplayName("Созвездие")]
        public virtual Constellations Cons { get; set; }
        public virtual ICollection<Galaxies> Galaxies { get; set; }
    }
}